var oprintAjax = null;
window.onload = function () {
    //打印服务启动成功检测
    var re = {stauts: "start", desc: '请确认打印面单程序是否启动！'}
    parent.postMessage(re, "*");
    //这里进行HTTP请求
    try {
        oprintAjax = new XMLHttpRequest();
    } catch (e) {
        oprintAjax = new ActiveXObject("Microsoft.XMLHTTP");
    }
    oprintAjax.onreadystatechange = function () {
        //当状态为4的时候，执行以下操作
        if (oprintAjax.readyState == 4) {
            parent.postMessage(JSON.parse(oprintAjax.responseText), "*");
        }
    };
    oprintAjax.onerror = function () {
        var re = {stauts: "noStart", desc: '请确认打印面单程序是否启动！'}
        parent.postMessage(re, "*");
    }
}

function postData(event) {
    alert(event.origin)
    if (event.origin == "http://www.ddyunf.com") {
        var str = JSON.stringify(event.data);
        //post方式打开文件
        oprintAjax.open('post', 'http://localhost:8088', true);
        oprintAjax.setRequestHeader("Content-type", "application/json");
        //post发送数据
        oprintAjax.send(str);
    }
}