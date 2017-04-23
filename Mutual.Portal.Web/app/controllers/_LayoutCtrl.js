
(function () {
    'use strict';

    function _layoutCtrl($scope, localStorageService, userService) {
        var vm = this;
        $scope.title = 'Homepage';
        $scope.username = undefined;

        vm.logout = function() {
            var result = userService.logout();
            if (result) {
                $scope.username = undefined;
                window.location = '/';
            }
        };

        vm.gotoLogin = function() {
            debugger;
            window.location = '/login';
        };

        vm.gotoMyProfile = function () {
            var userInfo = localStorageService.get('user_info');

            if (userInfo) {
                if (userInfo.employmentType === 1) {
                    window.location = '/nursingEmployeeDetail';
                }

            }
        };
    }

    _layoutCtrl.$inject = ['$scope', 'localStorageService', 'userService'];

     angular.module('mutualApp').controller('_layoutCtrl', _layoutCtrl);
})();