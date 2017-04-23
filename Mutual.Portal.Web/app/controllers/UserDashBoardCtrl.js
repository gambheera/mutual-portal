
(function () {
    'use strict';

    function userDashBoardCtrl($scope, userService) {
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
        };

        init();
    }

    userDashBoardCtrl.$inject = ['$scope', 'userService'];

    angular.module('mutualApp').controller('userDashBoardCtrl', userDashBoardCtrl);

})();