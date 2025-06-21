var NEW_WINDOW_WIDTH = 800;
var NEW_WINDOW_HEIGHT = 600;

$(document).ready(function () {

	$('div.kep_ikonok a').click(function (e) {
		var top = (screen.height-(NEW_WINDOW_HEIGHT + 100))/2;
		var left = (screen.width-NEW_WINDOW_WIDTH)/2;
		var w = window.open($(this).attr('href'), '_blank',
			'width='+NEW_WINDOW_WIDTH+
			',height='+NEW_WINDOW_HEIGHT+
			',scrollbars=yes'+
			',left='+left+
			',screenX='+left+
			',top='+top+
			',screenY='+top
		);
		e.preventDefault();
	});

});