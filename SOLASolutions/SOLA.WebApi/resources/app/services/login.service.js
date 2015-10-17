(function () {
    'use strict';

    angular
		.module('SoLaSolutions')
		.factory('LoginService', LoginService);

    LoginService.$inject = ['$rootScope', '$http', '$log', '$timeout', 'localStorageService'];
    function LoginService($rootScope, $http, $log, $timeout, localStorageService) {
        var AUTH_USER = SOLA.localStorageAuthName;

        var _token = {};

        //interface of service
        var service = {};
        service.getCurrentUser = getCurrentUser;
        service.pingAPI = pingAPI;
        service.login = login;
        service.refresh = refresh;
        service.logout = logout;
        service.isAuthenticated = isAuthenticated;
        service.getAccessToken = getAccessToken;
        service.getRefreshToken = getRefreshToken;
        service.clearToken = clearToken;
        service.saveToken = saveToken;
        service.getToken = getToken;
        return service;

        //implementation of service
        function getCurrentUser(callBack) {
            $http.get(getUrl('/api/self')).
			success(function (response, status, headers, config) {
			    $log.log('Load the current user is successfully.');
			    callBack(response);
			}).
			error(function (response, status, headers, config) {
			    callBack(returnEmptyObject());
			});
        }

        function pingAPI(callBack) {
            $http.get(getUrl('/api/ping')).
			success(function (response, status, headers, config) {
			    callBack(returnData(response));
			}).
			error(function (response, status, headers, config) {
			    callBack(returnEmptyObject());
			});
        }

        function login(username, password, callback) {
            var credentialPost = {};
            credentialPost.username = username;
            credentialPost.password = password;
            credentialPost.client_id = "webapp";
            credentialPost.grant_type = 'password';

            var urlPost = getUrl('/oauth/token');
            doLogin(urlPost, credentialPost, true, callback);
        }

        function refresh(callback) {
            var credentialPost = {};
            credentialPost.grant_type = 'refresh_token';
            credentialPost.client_id = "webapp";
            credentialPost.refresh_token = getRefreshToken();

            var urlPost = getUrl('/oauth/token');
            doLogin(urlPost, credentialPost, false, callback);
        }

        function logout(callBack) {
            $http.get(getUrl('/api/logout')).
			success(function (response, status, headers, config) {
			    callBack(returnData(response));
			}).
			error(function (response, status, headers, config) {
			    callBack(returnEmptyObject());
			});
        }

        function isAuthenticated() {
            var bLogined = false
            try {
                return (angular.isObject(getToken())
						&&
						angular.isDefined(getToken().access_token)
						&&
						angular.isDefined(getToken().refresh_token));
            } catch (e) { $log.log(e); }

            return bLogined;
        }

        function getAccessToken() {
            if (isAuthenticated()) return getToken().access_token;
            return null;
        }

        function getRefreshToken() {
            if (isAuthenticated()) return getToken().refresh_token;
            return null;
        }

        function clearToken() {
            try {
                SOLA.currentUser = {};
                _token = {};
                delete $http.defaults.headers.common.Authorization;
                localStorageService.remove(AUTH_USER);
            } catch (e) { $log.log(e); }
        }

        function saveToken(token) {
            try {
                _token = token;
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + _token.access_token;
                localStorageService.set(AUTH_USER, angular.toJson(_token));
            } catch (e) { $log.log(e); }
        }

        function getToken() {
            if (!angular.isObject(_token) || !angular.isDefined(_token.access_token) || !angular.isDefined(_token.refresh_token)) {
                _token = angular.fromJson(localStorageService.get(AUTH_USER));
            }
            return _token
        }

        //===================== private function ==================================
        function doLogin(urlPost, credentialPost, ignoreAuthModule, callback) {
            clearToken();

            var userLogin = { success: false, date: new Date() };
            // set header authentication
            var headers = {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            };
            //ignoreAuthModule=true --> current page is login
            $http({
                method: 'POST',
                url: urlPost,
                data: credentialPost,
                ignoreAuthModule: ignoreAuthModule,
                headers: headers,
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                }
            })
			.then(function (response, status, headers, config) {
			    if (angular.isObject(response.data)) {
			        if (angular.isDefined(response.data.access_token) && angular.isDefined(response.data.refresh_token)) {
			            userLogin.success = true;
			            userLogin.token = response.data;

			            saveToken(userLogin.token);

			            $log.log('Login successfully!!!');
			            callback(userLogin);

			        } else {
			            userLogin = {
			                success: false,
			                code: response.data.code,
			                message: response.data.message,
			                status: response.status
			            };

			            callback(userLogin);
			        }
			    } else {
			        userLogin = {
			            success: false,
			            code: response.status,
			            message: response.statusText,
			            status: response.status
			        };

			        callback(userLogin);
			    }

			}, function (response, status, headers, config) {
			    if (response.status == 500) {
			        userLogin = {
			            success: false,
			            code: response.status,
			            message: response.statusText,
			            status: response.status
			        };
			    } else
			        if (angular.isObject(response.data)) {
			            userLogin = {
			                success: false,
			                code: response.data.error,
			                message: response.data.error_description,
			                status: response.status
			            };
			        }

			    $log.log('Login failure!!!');
			    callback(userLogin);
			});
        }

        function returnEmptyObject() {
            return {};
        }

        function returnEmptyArray() {
            return [];
        }

        function returnData(response) {
            if (angular.isDefined(response.code) && angular.isDefined(response.message)) {
                var oData = { success: true, code: response.code, message: response.message };
                if (angular.isDefined(response.data)) oData.data = response.data;

                return oData;
            } else {
                var pCode = "Unknown";
                var pMessage = "Unknown Exception!!!";
                if (angular.isDefined(response.status)) pCode = pCode;
                if (angular.isDefined(response.statusText)) pMessage = statusText;

                return { success: false, code: pCode, message: pMessage };
            }
        }
    }

})();