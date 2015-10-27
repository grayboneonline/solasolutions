function getUrl(path) {
    return (SOLA.siteUrl + path);
}

function getUrlApi(path) {
    return (SOLA.siteUrl + SOLA.urlApi + path);
}

function getResourceContentPath(path) {
    return (SOLA.baseUrlResources + path + '?rnd=' + Math.random())
}

function getSuccess(response) {
    if (angular.isDefined(response.type) && angular.isDefined(response.message)) {
        return ({ success: true, code: response.type, messages: response.messages, data: response.data });
    } else {
        var code = "Unknown";
        var message = "Unknown Exception!!!";
        if (angular.isDefined(response.status)) code = code;
        if (angular.isDefined(response.statusText)) message = statusText;

        return { success: false, code: pCode, message: message };
    }
}

function getError(response) {
    return ({ success: false, code: status, error: response });
}