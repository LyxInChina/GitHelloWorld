//phantomjs 
var URL = "http://www.baidu.com/"
var page = require("webpage").create();
phantom.outputEncoding = "gbk";
page.open(URL, function (status) {
    if (status == "success") {
        console.log("success");
        console.log(page.title);
    } else {
        console.log("failed");
    }
    phantom.exit(0);
});


