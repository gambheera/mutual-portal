(function () {
    'use strict';

    function nurseService($http, $q, localStorageService, httpService) {

        function getHospitalList() {
            return httpService.get('/api/nurse/get-hospital-list').then(function (response) {
                return response;
            });
        };

        function getIndividualUserDetails(requesteeGuid) {
            return httpService.get('/api/nurse/get-individual-nurse?requesteeGuid=' + requesteeGuid).then(function(respond) {
                return respond;
            });
        }

        var service = {
            getIndividualUserDetails: getIndividualUserDetails,
            getHospitalList: getHospitalList
        };

        return service;
    }

    nurseService.$inject = ['$http', '$q', 'localStorageService', 'httpService'];

    angular.module('mutualApp').factory('nurseService', nurseService);
})();