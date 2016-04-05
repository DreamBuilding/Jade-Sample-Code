(function() {
    'use strict';

    angular
        .module('app', [])
        .controller('FormatController', [
            '$scope', 'ConverterFactory', function($scope, conversionFactory) {
                $scope.customer = {
                    firstname: '',
                    lastname: '',
                    amount: 0,
                    currency: ''
                };

                $scope.updatedCustomer = {
                    fullname: '',
                    amount: 0,
                    currency: '',
                    text: ''
                };

                var clearResults = function() {
                    $scope.updatedCustomer.fullname = '';
                    $scope.updatedCustomer.amount = '';
                    $scope.updatedCustomer.currency = '';
                    $scope.updatedCustomer.text = '';

                    $scope.isAmountValid = true; //Reset until server evaluates value
                }

                $scope.currencies = ['AU', 'US']; //This could be moved to a backend call and then validate server side.

                $scope.convert = function() {
                    clearResults();

                    $scope.isBusy = true; // Hide the results panel until server returns successfully.

                    $scope.isCurrencyValid = $.inArray($scope.customer.currency, $scope.currencies) >= 0; //currency is not part of the server code so validate against existing values.

                    if ($scope.isCurrencyValid) { //only make server call if currency is valid.
                        conversionFactory.convert($scope.customer.amount)
                            .success(function(words) {
                                $scope.updatedCustomer.fullname = $scope.customer.firstname + ' ' + $scope.customer.lastname;
                                $scope.updatedCustomer.currency = $scope.customer.currency;
                                $scope.updatedCustomer.amount = $scope.customer.amount;
                                $scope.updatedCustomer.text = words;
                                $scope.isBusy = false;
                            }).error(function(data, status) {
                                if (status === 400) {
                                    $scope.isAmountValid = false;
                                } else {
                                    alert(data.message); //if this is an unexpected error than alert for now.
                                }
                            });
                    }
                };

                $scope.isAmountValid = true;

                $scope.isCurrencyValid = true;

                $scope.isBusy = true;
            }
        ])
        .directive('updatedCustomer', function () {
            return {
                templateUrl: 'results.html'
            };
        });

})(window.angular);