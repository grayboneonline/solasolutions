(function () {
    'use strict';
    var app =
    angular.module('sola-directive', []);

    /*
     * collapse
     * use collapse and expand an element
     * */
    app.directive('collapse', ['$animate', function ($animate) {
        return {
            link: function (scope, element, attrs) {
                function expand() {
                    element.removeClass('collapse').addClass('collapsing');
                    $animate.addClass(element, 'in', {
                        to: { height: element[0].scrollHeight + 'px' }
                    }).then(expandDone);
                }

                function expandDone() {
                    element.removeClass('collapsing');
                    element.css({ height: 'auto' });
                }

                function collapse() {
                    element
			        .css({ height: element[0].scrollHeight + 'px' })
			        .removeClass('collapse')
			        .addClass('collapsing');

                    $animate.removeClass(element, 'in', {
                        to: { height: '0' }
                    }).then(collapseDone);
                }

                function collapseDone() {
                    element.css({ height: '0' });
                    element.removeClass('collapsing');
                    element.addClass('collapse');
                }

                scope.$watch(attrs.collapse, function (shouldCollapse) {
                    if (shouldCollapse) {
                        collapse();
                    } else {
                        expand();
                    }
                });
            }
        };
    }]);

    /*
     * elementheight
     * set height of element base on height of parent element
     * */
    app.directive('elementheight', function ($window) {
        return function (scope, element) {
            var e = angular.element($window);
            scope.getElementDimensions = function () {
                var windowHeight = $window.innerHeight;
                var toolbarHeight = document.getElementsByClassName('top-menu-header')[0].clientHeight;
                var parent = element.parent()[0];
                var parentHeight = 0;
                if (windowHeight - toolbarHeight < parent.offsetHeight)
                    parentHeight = windowHeight - toolbarHeight;
                else
                    parentHeight = parent.offsetHeight;

                var heightOfAnotherElement = 0;
                for (var i = 0; i < parent.childElementCount; i++) {
                    if (parent.children[i] != element[0])
                        heightOfAnotherElement += parent.children[i].offsetHeight;
                }
                return {
                    'h': parentHeight - heightOfAnotherElement
                };
            };
            scope.$watch(scope.getElementDimensions, function (newValue, oldValue) {
                element[0].style.height = newValue.h + 'px';
            }, true);

            e.bind('resize', function () {
                if (!scope.$$phase) scope.$apply();
            });
        }
    });

    /*
     * 
     * */
    app.directive('scrolly', function () {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                var raw = element[0];

                element.bind('scroll', function () {
                    if (raw.scrollTop + raw.offsetHeight >= raw.scrollHeight) {
                        if (!scope.$$phase) scope.$apply(attrs.scrolly);
                    }
                });
            }
        };
    });

    /*
     * 
     * */
    app.directive('solaLoading', ['$http', '$rootScope', function ($http, $rootScope) {
        return {
            link: function (scope, elm, attrs) {
                $rootScope.spinnerActive = false;
                scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function (loading) {
                    $rootScope.spinnerActive = loading;
                    if (loading) {
                        elm.removeClass('ng-hide');
                    } else {
                        elm.addClass('ng-hide');
                    }
                });
            }
        };
    }]);
})();