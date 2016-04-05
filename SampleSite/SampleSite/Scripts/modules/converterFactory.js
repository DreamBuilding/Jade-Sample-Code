(function () {
    'use strict';

    angular
        .module('app')
        .factory('ConverterFactory', converterFactory);

    converterFactory.$inject = ['$http'];

    function converterFactory($http) {
        var service = {
            convert: getData
        };

        return service;

        function getData(value) {
            return $http.post('/api/' + value +  '/convert-to-word', null);
        }
    }
})(window.angular);
