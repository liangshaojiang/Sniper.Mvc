
//��ʼ
var onbegin = function () {
    $.MyCommon.PageLoading({ loadingTips: "���ڴ������Ժ�..." });
}

//���
var oncomplete = function (request, status) {
    $.MyCommon.Remove();
}


//ʧ��
var onfailure = function (request, error) {
    alert(error);
    $.MyCommon.Remove();
}

//�ɹ�
var onsuccess = function (data) {
    alert(data.Message);
    $.MyCommon.Remove();
    if (data.Status) {
        window.location.reload();
    }
}




 


