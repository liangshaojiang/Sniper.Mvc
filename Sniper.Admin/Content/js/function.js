
//开始
var onbegin = function () {
    $.MyCommon.PageLoading({ loadingTips: "正在处理，请稍候..." });
}

//完成
var oncomplete = function (request, status) {
    $.MyCommon.Remove();
}


//失败
var onfailure = function (request, error) {
    alert(error);
    $.MyCommon.Remove();
}

//成功
var onsuccess = function (data) {
    alert(data.Message);
    $.MyCommon.Remove();
    if (data.Status) {
        window.location.reload();
    }
}




 


