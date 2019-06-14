// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function isDate(txtDate) {
	var currVal = txtDate;
	if (currVal == '')
		return false;
	var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
	var dtArray = currVal.match(rxDatePattern);

	if (dtArray == null)
		return false;

	dtMonth = dtArray[1];
	dtDay = dtArray[3];
	dtYear = dtArray[5];

	if (dtMonth < 1 || dtMonth > 12)
		return false;
	else if (dtDay < 1 || dtDay > 31)
		return false;
	else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
		return false;
	else if (dtMonth == 2) {
		var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
		if (dtDay > 29 || (dtDay == 29 && !isleap))
			return false;
	}
    return true;

}

$(document).ready(function () {
    $('.history-button').click(function () {
        $('.history-feature').addClass('display-popup');
        
    });
    $('.close-history').click(function () {
        $('.history-feature').removeClass('display-popup');

    });
    
});

