
(function () {
    'use strict';

    function nursingEmployeeDetailCtrl($scope, $q, localStorageService, userService, nurseService) {
        var vm = this;
        vm.title = 'Homepage';

        vm.employeeTypeList = [{ value: 1, text: 'Nurse' }/*, { value: 2, text: 'Teacher' }, { value: 3, text: 'Govenment' }*/];

        vm.selectedEmployeeType = 0;
        vm.name = "";
        vm.mobileNumber = "";
        vm.telephoneNumber = "";
        vm.userGuid = "";

        vm.hospitalList = [];
        vm.dreamHospitalList = [];

        vm.currentHospital = { id: 0, name: '' };
        vm.myDreamHospitalList = [];

        function getNurseInfo() {
            debugger;
            var userInfo = localStorageService.get('user_info');

            if (!userInfo) {
                window.location = '/';
                return;
            }

            nurseService.getIndividualUserDetails(userInfo.guidString).then(function(success) {
                var nurseObj = success.data;
                debugger;
            }, function(error) {
                
            });
        };

        function getHospitalList() {
            // var deferred = $q.defer();
            nurseService.getHospitalList().then(function(response) {
                vm.hospitalList = response.data.data;
                vm.dreamHospitalList = response.data.data;
                // deferred.resolve(true);
            });

            // return deferred.promise;
        };

        function init() {
            $scope.$parent.username = userService.authenticateCurrentPosition();
            getHospitalList();
            getNurseInfo();
        };

        init();
    }

    nursingEmployeeDetailCtrl.$inject = ['$scope', '$q', 'localStorageService', 'userService', 'nurseService'];

    angular.module('mutualApp').controller('nursingEmployeeDetailCtrl', nursingEmployeeDetailCtrl);

})();