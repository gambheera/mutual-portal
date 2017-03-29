
(function () {
    'use strict';
    angular.module('mutualApp').controller('_LayoutCtrl', _LayoutCtrl);

    _LayoutCtrl.$inject = ['$scope'];

    function _LayoutCtrl($scope) {
        var vm = this;
        $scope.title = 'Homepage';
    }
})();