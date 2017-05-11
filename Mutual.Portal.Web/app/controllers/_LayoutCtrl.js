
(function () {
    'use strict';

    function _layoutCtrl($scope, swalService, localStorageService, userService) {
        var vm = this;
        $scope.title = 'Homepage';
        $scope.username = undefined;
        vm.remainingViewCount = 0;

        $scope.getUserSimpleInfo = function() {
            userService.getUserSimpleInfo().then(function (data) {
                console.log(data);
                if (!data.data.metaData.isSucceeded) return;

                vm.remainingViewCount = data.data.data.myRemainingViewCount;
                console.log(vm.remainingViewCount);
            });
        };

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

        vm.showRemainingViewCount = function () {
            var message = "You have " + vm.remainingViewCount + " remaining chances to view others' contact information";
            var header = "You have " + vm.remainingViewCount + " more chances";
            swalService.info(message, header);
        };

        var init = function() {
            $scope.getUserSimpleInfo();
        };

        init();
    }

    _layoutCtrl.$inject = ['$scope', 'swalService', 'localStorageService', 'userService'];

     angular.module('mutualApp').controller('_layoutCtrl', _layoutCtrl);
})();