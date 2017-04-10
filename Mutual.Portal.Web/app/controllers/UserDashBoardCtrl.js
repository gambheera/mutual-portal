
(function () {
    'use strict';

    function userDashBoardCtrl($scope, $rootScope, userService) {
        var vm = this;
        vm.title = 'Homepage';

        vm.getUserInfo = function () {
            userService.getUserInfo().then(function (successRespond) {
                console.log(successRespond);
            }, function (errorRespond) {
                console.log(errorRespond);
            });
        };

        function init() {

            $scope.$parent.username = userService.authenticateCurrentPosition();
            debugger;
            
        };

        init();
    }

    userDashBoardCtrl.$inject = ['$scope', '$rootScope', 'userService'];

    angular.module('mutualApp').controller('userDashBoardCtrl', userDashBoardCtrl);

})();