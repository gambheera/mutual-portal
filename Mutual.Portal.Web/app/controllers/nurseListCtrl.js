
(function () {
    'use strict';

    function nurseListCtrl($scope, $q, localStorageService, toastrService, swalService, userService, nurseService) {
        var vm = this;
        vm.title = 'Homepage';
        vm.currentHospital = {};
        vm.dreamHospital = {};
        vm.pageNumber = 0;

        vm.hospitalList = [];

        vm.searchedResultList = [];

        function searchNursesInternaly(currentHospitalId, dreamHospitalId, pageNumber) {
            var deferred = $q.defer();
            vm.searchedResultList = [];

            if (currentHospitalId === undefined) currentHospitalId = 0;
            if (dreamHospitalId === undefined) dreamHospitalId = 0;

            nurseService.searchNurses(currentHospitalId, dreamHospitalId, pageNumber).then(function (success) {
                var searchedResultList = success.data.data;
                for (var i = 0; i < searchedResultList.length; i++) {
                    var obj = searchedResultList[i];
                    var hospitalList = "";
                    for (var j = 0; j < obj.dreamHospitalList.length; j++) {
                        var tempHospital = obj.dreamHospitalList[j].hospital;
                        if (j === (obj.dreamHospitalList.length - 2)) {
                            hospitalList = hospitalList + tempHospital.name + " and ";
                        } else if (j === (obj.dreamHospitalList.length - 1)) {
                            hospitalList = hospitalList + tempHospital.name;
                        } else {
                            hospitalList = hospitalList + tempHospital.name + ", ";
                        }
                    }

                    var description = "I'm currently woring at " + obj.hospital.name + " (" + obj.hospital.districtString + " district). I'm expecting to go " + hospitalList;
console.log(obj);
                    var element = {
                        description: description,
                        code: obj.user.code,
                        lastLogin: obj.user.lastLoginOnString,
                        guid: obj.user.guidString,
                        viewCount: (obj.user.myCurrentViewCount + obj.user.myTotalViewCount)
                    };
                    
                    vm.searchedResultList.push(element);

                    if (vm.searchedResultList.length > 0) {
                        vm.pageNumber = pageNumber;
                    }
                    deferred.resolve(true);
                }
            }, function (failed) {
                deferred.resolve(false);
                toastrService.error('Failed to load. Please try again.');
            });

            return deferred.promise;
        };

        vm.searchNurses = function() {
            if (vm.currentHospital === undefined) return;
            if (vm.dreamHospital === undefined) return;

            searchNursesInternaly(vm.currentHospital.id, vm.dreamHospital.id, vm.pageNumber);
        };

        vm.nextPage = function () {
            if (vm.pageNumber <= 0) vm.pageNumber = 0;

            var currentHospitalId = vm.currentHospital.id;
            var dreamHospitalId = vm.dreamHospital.id;
            var pageToBe = vm.pageNumber + 1;

            searchNursesInternaly(currentHospitalId, dreamHospitalId, pageToBe).then(function(respond) {
                // if (!respond) vm.pageNumber = vm.pageNumber - 1;
            });
        };

        vm.previousPage = function () {
            if (vm.pageNumber <= 0) return; // vm.pageNumber = 0;

            var currentHospitalId = vm.currentHospital.id;
            var dreamHospitalId = vm.dreamHospital.id;
            var pageToBe = vm.pageNumber;

            if (vm.pageNumber > 1) pageToBe = vm.pageNumber - 1;
            
            searchNursesInternaly(currentHospitalId, dreamHospitalId, pageToBe).then(function(respond) {
                // if (!respond) vm.pageNumber = vm.pageNumber + 1;
            });
        };

        vm.gotoIndividualProfilePage = function (guid) {
            localStorageService.set('requestee-guid', guid)
            window.location = '/viewprofile';
        };

        function getHospitalList() {
            nurseService.getHospitalList().then(function (success) {
                vm.hospitalList = success.data.data;

                var allObj = { id: 0, name: '_ANY' }

                vm.hospitalList.unshift(allObj);

            }, function(fail) {
                
            });
        };

        function init() {
            $scope.$parent.username = userService.authenticateCurrentPosition();
            searchNursesInternaly(0, 0, 1);
            getHospitalList();
        };

        init();
    }

    nurseListCtrl.$inject = ['$scope', '$q', 'localStorageService', 'toastrService', 'swalService', 'userService', 'nurseService'];

    angular.module('mutualApp').controller('nurseListCtrl', nurseListCtrl);

})();