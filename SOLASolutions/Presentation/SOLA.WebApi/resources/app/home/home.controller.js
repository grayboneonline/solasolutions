(function () {
    'use strict';

    angular.module('SoLaSolutions')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location', '$translate', '$mdToast', '$mdDialog', 'authService', 'LoginService'];
    function LoginController($location, $translate, $mdToast, $mdDialog, authService, LoginService) {

        //initialize the form controller
        (function initController() {
            //get & process page change password
            var objParams = $location.search();
            if (angular.isObject(objParams)) {
                if (angular.isDefined(objParams.tokenInvalid) && angular.isDefined(objParams.tokenExpiredMinutes)) {
                    var expiredTime = (new Date()).getTime() + objParams.tokenExpiredMinutes * 60000;
                    showExpiredDialog(expiredTime);
                }
                else if (angular.isDefined(objParams.passwordChangedOk) && angular.isDefined(objParams.email)) {
                    $mdDialog.show(
						$mdDialog.alert()
				        	.clickOutsideToClose(true)
				        	.title('Password Set')
				        	.content("You have successfully set your new password.")
				        	.ok('Ok')
				    );
                }
            }

            if (LoginService.isAuthenticated()) {
                $location.path(SOLA.stateUrlHome);
            }
        })();

        var self = this;
        self.btnSignIn = true;

        self.login = function () {
            if (self.username == null || self.username == "" || self.password == null || self.password == "") {
                $mdToast.show(
					$mdToast.simple()
			        	.content($translate.instant('error.5'))
			        	.position('bottom right')
			        	.hideDelay(4000)
			    );
                return;
            }

            self.btnSignIn = false;

            LoginService.login(self.username, self.password, function (userLogin) {
                if (userLogin.success) {
                    authService.loginConfirmed('success');
                } else {
                    $mdToast.show(
						$mdToast.simple()
				        	.content(userLogin.message)
				        	.position('bottom right')
				        	.hideDelay(4000)
				    );
                    self.btnSignIn = true;
                }
            });
        }
    }

})();