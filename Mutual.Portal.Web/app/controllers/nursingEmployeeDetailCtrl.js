
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
        vm.userCode = "";

        vm.hospitalList = [];
        vm.dreamHospitalList = [];

        vm.currentHospital = { id: 0, name: "" };
        vm.myDreamHospitalList = [];
        vm.selectedMyDreamHospital = {};

        var previousCurrentHospital = undefined;

        function getNurseInfo() {
            var deferred = $q.defer();
            var userInfo = localStorageService.get('user_info');

            if (!userInfo) {
                window.location = '/';
                return;
            }

            nurseService.getIndividualUserDetails(userInfo.guidString).then(function(success) {
                var nurseObj = success.data.data;
                vm.name = nurseObj.user.name;
                vm.userCode = nurseObj.user.code;
                vm.mobileNumber = nurseObj.user.contactNumber1;
                vm.telephoneNumber = nurseObj.user.contactNumber2;

                for (var i = 0; i < vm.hospitalList.length; i++) {
                    if (nurseObj.hospital.id === vm.hospitalList[i].id) {
                        vm.currentHospital = vm.hospitalList[i];
                        break;
                    }
                }
                
                vm.myDreamHospitalList = nurseObj.dreamHospitalList;

                deferred.resolve(true);
            }, function(error) {
                
            });
            return deferred.promise;
        };

        function getHospitalList() {
             var deferred = $q.defer();
            nurseService.getHospitalList().then(function (response) {
                localStorageService.set('hospital_list', response.data.data);
                vm.hospitalList = [];
                vm.dreamHospitalList = [];
                vm.hospitalList = localStorageService.get('hospital_list');
                vm.dreamHospitalList = localStorageService.get('hospital_list');
                 deferred.resolve(true);
            });

             return deferred.promise;
        };

        function manageDropdownsInitially() {
            vm.currentHospitalChangeEvent().then(function() {

                // removing myDreamHospitalList items from current hospital selection list
                debugger;
                for (var j = 0; j < vm.myDreamHospitalList.length; j++) {
                    for (var i = 0; i < vm.hospitalList.length; i++) {
                        if (vm.hospitalList[i].id === vm.myDreamHospitalList[j].hospital.id) {
                            // should remove from the hospitalList
                            var index = vm.hospitalList.indexOf(vm.hospitalList[i]);

                            if (index < 0) break;

                            vm.hospitalList.splice(index, 1);
                            break;
                        }
                    }

                    debugger;
                    for (var k = 0; k < vm.dreamHospitalList.length; k++) {
                        if (vm.dreamHospitalList[k].id === vm.myDreamHospitalList[j].hospital.id) {
                            //should remove from the dreamHospitalList
                            var indexDream = vm.dreamHospitalList.indexOf(vm.dreamHospitalList[k]);

                            if (indexDream < 0) break;

                            vm.dreamHospitalList.splice(indexDream, 1);
                            break;
                        }
                    }
                }
            });
        };

        vm.addHospitalToMyDreamHospitalList = function () {
            if (vm.selectedMyDreamHospital === undefined) return;

            // adding to myDreamHospitalList
            var dreamHospitalObj = { id:0, hospital: vm.selectedMyDreamHospital };
            vm.myDreamHospitalList.push(dreamHospitalObj);

            debugger;

            var selectedHospitalFromHospitalList = {};

            for (var i = 0; i < vm.hospitalList.length; i++) {
                if (vm.hospitalList[i].id === vm.selectedMyDreamHospital.id) {
                    selectedHospitalFromHospitalList = vm.hospitalList[i];
                    break;
                }
            }

            // removing from dreamHospitalList
            var index = vm.dreamHospitalList.indexOf(vm.selectedMyDreamHospital);
            var indexCurrent = vm.hospitalList.indexOf(selectedHospitalFromHospitalList);

            if (index < 0) return;
            vm.dreamHospitalList.splice(index, 1);
            vm.hospitalList.splice(indexCurrent, 1);
        };

        vm.removeHospitalFromMyDreamHospitalList = function (dreamHospital) {
            // removing from myDreamHospitalList
            var index = vm.myDreamHospitalList.indexOf(dreamHospital);
            if (index < 0) return;
            vm.myDreamHospitalList.splice(index, 1);

            // adding to dreamHospitalList
            vm.dreamHospitalList.push(dreamHospital.hospital);
            vm.hospitalList.push(dreamHospital.hospital);
        };

        vm.currentHospitalChangeEvent = function () {
            var defered = $q.defer();
            if (previousCurrentHospital !== undefined) {
                vm.dreamHospitalList.push(previousCurrentHospital);
            }

            var removalEligibilityHospital = {};

            for (var i = 0; i < vm.dreamHospitalList.length; i++) {
                if (vm.dreamHospitalList[i].id === vm.currentHospital.id) {
                    removalEligibilityHospital = vm.dreamHospitalList[i];
                    break;
                }
            }

            var index = vm.dreamHospitalList.indexOf(removalEligibilityHospital);
            if (index < 0) return;

            vm.dreamHospitalList.splice(index, 1);

            previousCurrentHospital = vm.currentHospital;

            defered.resolve(true);

            return defered.promise;
        };

        vm.saveEmployeeDetails = function () {
            if (vm.myDreamHospitalList.length <= 0) {
                alert("Please select at least one dream hospital");
                return;
            }

            var userObj = {
                name: vm.name,
                contactNumber1: vm.mobileNumber,
                contactNumber2: vm.telephoneNumber
            };

            var obj = {
                user: userObj,
                hospital: vm.currentHospital,
                DreamHospitalList: vm.myDreamHospitalList
            };

            nurseService.saveNurse(obj).then(function(success) {
                alert(success.data.data);
            }, function(fail) {
                
            });
        };

        function init() {
            debugger;
            $scope.$parent.username = userService.authenticateCurrentPosition();
            getHospitalList().then(function() {
                getNurseInfo().then(function() {
                    manageDropdownsInitially();
                });
            });
            
        };

        init();
    }

    nursingEmployeeDetailCtrl.$inject = ['$scope', '$q', 'localStorageService', 'userService', 'nurseService'];

    angular.module('mutualApp').controller('nursingEmployeeDetailCtrl', nursingEmployeeDetailCtrl);

})();