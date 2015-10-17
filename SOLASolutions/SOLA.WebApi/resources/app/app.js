(function () {
    'use strict';

    angular.module('SoLaSolutions', ['pascalprecht.translate',
                                    'ui.router',
  	                                'ngMaterial',
								    'ngAnimate',
								    'ngTouch',
  	                                'ab-base64',
                                    'oc.lazyLoad',
  	                                'LocalStorageModule',
  	                                'http-auth-interceptor',
                                    'sola-directive'])
        //.config(configTranslateMessage)
        .config(configLocalStorage)
  		.config(configLeftMenu)
		.config(configCORS)
		//.config(configHttpInterceptor)
  		.run(runApp)
    ;

    configTranslateMessage.$inject = ['$translateProvider'];
    function configTranslateMessage($translateProvider) {
        $translateProvider.useUrlLoader(getUrl('/messageBundle'));
        $translateProvider.preferredLanguage('en');
        $translateProvider.fallbackLanguage('en');
        $translateProvider.useSanitizeValueStrategy(null);
    }

    /**
  	 * Configuration the local storage provider
  	 */
    configLocalStorage.$inject = ['localStorageServiceProvider'];
    function configLocalStorage(localStorageServiceProvider) {
        localStorageServiceProvider
          .setPrefix(SOLA.siteName) //should be the application id from server side
          //.setStorageType('sessionStorage')
          .setNotify(true, true)
    } //configLocalStorage

    configLeftMenu.$inject = ['$mdThemingProvider', '$stateProvider', '$urlRouterProvider', '$mdIconProvider'];
    function configLeftMenu($mdThemingProvider, $stateProvider, $urlRouterProvider, $mdIconProvider) {

        $mdThemingProvider
			.theme('default')
			.primaryPalette(SOLA.primaryPalette)
			.accentPalette(SOLA.accentPalette);

        $stateProvider
			.state('login', {
			    url: "/login",
			    views: {
			        'main': {
			            templateUrl: getResourceContentPath("/app/login/login.html")
			        }
			    },
			    resolve: {
			        loadMyService: ['$ocLazyLoad', function ($ocLazyLoad) {
			            return $ocLazyLoad.load(
                            {
                                name: 'controller',
                                files: ['resources/app/login/login.controller.js']
                            }
                        );

			        }]
			    }
			})
			.state('app', {
			    url: "/app",
			    views: {
			        'main': {
			            templateUrl: getResourceContentPath("/app/main/main.html")
			        }
			    },
			    resolve: {
			        loadMyService: ['$ocLazyLoad', function ($ocLazyLoad) {
			            return $ocLazyLoad.load(
                            {
                                name: 'controller',
                                files: ['resources/app/main/main.controller.js']
                            }
                        );

			        }]
			    }
			});

        // For any unmatched url, send to /index
        $urlRouterProvider.otherwise(function ($injector, $location) {
            var $state = $injector.get("$state");
            $state.go("login");
        });
    }

    configCORS.$inject = ['$sceDelegateProvider', '$ocLazyLoadProvider'];
    function configCORS($sceDelegateProvider, $ocLazyLoadProvider) {
        $sceDelegateProvider.resourceUrlWhitelist([
			// Allow same origin resource loads.
			'self'
            //,
			// Allow loading from outer templates domain.
			//SOLA.cdnEndpoint + '/**'
        ]);

        $ocLazyLoadProvider.config({
            asyncLoader: $script
        });
    }

    configHttpInterceptor.$inject = ['$httpProvider', '$provide'];
    function configHttpInterceptor($httpProvider, $provide) {
        $provide.factory('myHttpInterceptor', function ($q, $rootScope, $log, localStorageService, authService) {
            return {
                'request': function (config) {
                    // intercept and change config: e.g. change the URL
                    // broadcasting 'httpRequest' event
                    
                    if (isUrlApi(config.url)) {
                        var AUTH_USER = SOLA.localStorageAuthName;
                        var token = angular.fromJson(localStorageService.get(AUTH_USER));
                        if (angular.isDefined(token) && angular.isObject(token)) {
                            config.headers["Authorization"] = "Bearer " + token.access_token;
                        } else {
                            $log.log("Can't find token for authorization URL:" + config.url);
                            authService.loginCancelled();
                        }
                    }

                    $rootScope.$broadcast('httpRequest', config);
                    return config || $q.when(config);
                },
                'response': function (response) {
                    // we can intercept and change response here...
                    // broadcasting 'httpResponse' event
                    $rootScope.$broadcast('httpResponse', response);
                    return response || $q.when(response);
                },
                'requestError': function (rejection) {
                    // broadcasting 'httpRequestError' event
                    $rootScope.$broadcast('httpRequestError', rejection);
                    return $q.reject(rejection);
                },
                'responseError': function (rejection) {
                    // broadcasting 'httpResponseError' event
                    $rootScope.$broadcast('httpResponseError', rejection);
                    return $q.reject(rejection);
                }
            };
        });
        $httpProvider.interceptors.push('myHttpInterceptor');
    }

    runApp.$inject = ['$rootScope', '$state', '$injector', '$location', '$log', '$timeout', 'authService', '$window', '$mdColorPalette', '$mdDialog'];
    function runApp($rootScope, $state, $injector, $location, $log, $timeout, authService, $window, $mdColorPalette, $mdDialog) {

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams, error) {
            //if it can't login state
            /*
            if (toState !== null && toState.url !== null && toState.url !== "" && toState.url.indexOf("/login") < 0) {
                if (LoginService.isAuthenticated()
					&& angular.isObject(SOLA.currentUser)
					&& angular.isUndefined(SOLA.currentUser.userId)) {

                    event.preventDefault();
                    LoginService.getCurrentUser(function (oUser) {
                        if (angular.isObject(oUser) && angular.isDefined(oUser.userId)) {
                            SOLA.currentUser = oUser;
                            UserSharedService.prepForBroadcast(SOLA.currentUser);

                            $state.go(toState, toParams);
                        } else {
                            authService.loginCancelled();
                        }
                    });
                } else {
                    var pageList = UserSharedService.pageList;
                    if (angular.isUndefined(pageList) || pageList == null || pageList.length == 0)
                        $location.path(SOLA.stateUrlHome);

                    var hasPermission = false;
                    for (var i = 0; i < pageList.length; i++) {
                        var page = pageList[i];
                        if (toState.name == page.stateName) {
                            hasPermission = true;
                            break;
                        }
                        if (angular.isDefined(page.pageChild) && page.pageChild != null) {
                            for (var j = 0; j < page.pageChild.length; j++) {
                                var child = page.pageChild[j];
                                if (toState.name == child.stateName) {
                                    hasPermission = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!hasPermission) {
                        event.preventDefault();
                        if (angular.isUndefined(fromState) || fromState == null || fromState.name == "")
                            $state.go("app.home");
                        else
                            $state.go(fromState.name);
                    }
                    $mdDialog.cancel();
                }
            }
            else if (toState !== null && toState.url !== null && toState.url !== "" && toState.url.indexOf("/login") > -1) {
                if (LoginService.isAuthenticated()
						&& angular.isObject(SOLA.currentUser)
						&& angular.isDefined(SOLA.currentUser.userId)) {

                    event.preventDefault();
                    if (angular.isUndefined(fromState) || fromState == null || fromState.name == "")
                        $state.go("app.home");
                    else
                        $state.go(fromState.name);
                }
            }
            */


        });

        $rootScope.$on('event:auth-loginConfirmed', function () {
            $location.path(SOLA.stateUrlHome);
        });

        $rootScope.$on('event:auth-loginCancelled', function () {
            LoginService.clearToken();
            $window.location = getUrlLogin();
        });

        $rootScope.$on('event:auth-loginRequired', function () {
            LoginService.refresh(function (userLogin) {
                if (userLogin.success) {
                    authService.loginConfirmed('success', function (config) {
                        config.headers["Authorization"] = "Bearer " + userLogin.token.access_token;
                        return config;
                    });
                } else {
                    authService.loginCancelled();
                }
            });
        });

    } //runApp

})();//end file
