
$(function () {
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editableform.loading = "<div class='editableform-loading'><i class='ace-icon fa fa-spinner fa-spin fa-2x light-blue'></i></div>";
    $.fn.editableform.buttons = '<button type="submit" class="btn btn-info editable-submit"><i class="ace-icon fa fa-check"></i></button>' +
                                '<button type="button" class="btn editable-cancel"><i class="ace-icon fa fa-times"></i></button>';

    try {
        try {
            document.createElement('IMG').appendChild(document.createElement('B'));
        } catch (e) {
            Image.prototype.appendChild = function (el) { }
        }

        var last_gritter
        $('#avatar').editable({
            type: 'image',
            name: 'avatar',
            value: null,
            image: {
                btn_choose: '请选择头像',
                droppable: true,
                maxSize: 110000,//~100Kb
                name: 'avatar',
                on_error: function (error_type) {
                    if (last_gritter) $.gritter.remove(last_gritter);
                    if (error_type == 1) {//格式错误 error
                        last_gritter = $.gritter.add({
                            title: '不是图片',
                            text: '请选择 jpg|gif|png 格式图片',
                            class_name: 'gritter-error gritter-center'
                        });
                    } else if (error_type == 2) {//文件大小错误
                        last_gritter = $.gritter.add({
                            title: '图片太大!',
                            text: '上传的头像图片不能超过 100Kb!',
                            class_name: 'gritter-error gritter-center'
                        });
                    }
                    else {//other error
                    }
                },
                on_success: function () {
                    $.gritter.removeAll();
                }
            },
            url: function (params) {
                  
                var submit_url = UPLOAD_URL;//上传地址
                var deferred = null;
                var avatar = '#avatar';

                //if value is empty (""), it means no valid files were selected
                //but it may still be submitted by x-editable plugin
                //because "" (empty string) is different from previous non-empty value whatever it was
                //so we return just here to prevent problems
                var value = $(avatar).next().find('input[type=hidden]:eq(0)').val();
                if (!value || value.length == 0) {
                    deferred = new $.Deferred
                    deferred.resolve();
                    return deferred.promise();
                }

                var $form = $(avatar).next().find('.editableform:eq(0)')
                var file_input = $form.find('input[type=file]:eq(0)');
                var pk = $(avatar).attr('data-pk');//primary key to be sent to server

                var ie_timeout = null


                if ("FormData" in window) {
                    var formData_object = new FormData();// ajax 上传

                    //serialize our form (which excludes file inputs)
                    $.each($form.serializeArray(), function (i, item) {
                        //add them one by one to our FormData 
                        formData_object.append(item.name, item.value);
                    });
                    //and then add files
                    $form.find('input[type=file]').each(function () {
                        var field_name = $(this).attr('name');
                        var files = $(this).data('ace_input_files');
                        if (files && files.length > 0) {
                            formData_object.append(field_name, files[0]);
                        }
                    });

                    //append primary key to our formData
                    formData_object.append('pk', pk);

                    deferred = $.ajax({
                        url: submit_url,
                        type: 'POST',
                        processData: false,//important
                        contentType: false,//important
                        dataType: 'json',//server response type
                        data: formData_object
                    })
                }
                else {
                    deferred = new $.Deferred

                    var temporary_iframe_id = 'temporary-iframe-' + (new Date()).getTime() + '-' + (parseInt(Math.random() * 1000));
                    var temp_iframe =
                            $('<iframe id="' + temporary_iframe_id + '" name="' + temporary_iframe_id + '" \
			frameborder="0" width="0" height="0" src="about:blank"\
			style="position:absolute; z-index:-1; visibility: hidden;"></iframe>')
                            .insertAfter($form);

                    $form.append('<input type="hidden" name="temporary-iframe-id" value="' + temporary_iframe_id + '" />');

                    //append primary key (pk) to our form
                    $('<input type="hidden" name="pk" />').val(pk).appendTo($form);

                    temp_iframe.data('deferrer', deferred);
                    //we save the deferred object to the iframe and in our server side response
                    //we use "temporary-iframe-id" to access iframe and its deferred object

                    $form.attr({
                        action: submit_url,
                        method: 'POST',
                        enctype: 'multipart/form-data',
                        target: temporary_iframe_id //important
                    });

                    $form.get(0).submit();

                    //30秒过期
                    ie_timeout = setTimeout(function () {
                        ie_timeout = null;
                        temp_iframe.attr('src', 'about:blank').remove();
                        deferred.reject({ 'status': 'fail', 'message': 'Timeout!' });
                    }, 30000);
                }

                //结果处理
                deferred
                .done(function (result) {
                    //success 
                    if (result.status == 'OK') $(avatar).get(0).src = result.url;
                    else alert(res.message);
                })
                .fail(function (result) {//failure
                   // alert("There was an error");
                })
                .always(function () {//called on both success and failure
                    if (ie_timeout) clearTimeout(ie_timeout)
                    ie_timeout = null;
                });

                return deferred.promise();
            },

            success: function (response, newValue) {
            }
        })
    } catch (e) { }

});

