
(function () {
    'use strict';

    function _layoutCtrl($scope, $rootScope, userService) {
        var vm = this;
        $scope.title = 'Homepage';
        $scope.username;

        vm.logout = function() {
            var result = userService.logout();
            if (result) {
                $scope.username = undefined;
                window.location = '/';
            }
        };
    }

    _layoutCtrl.$inject = ['$scope', '$rootScope', 'userService'];

     angular.module('mutualApp').controller('_layoutCtrl', _layoutCtrl);
})();