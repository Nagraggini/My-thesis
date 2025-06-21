var DEF_NEW_WINDOW_WIDTH = 800;
var DEF_NEW_WINDOW_HEIGHT = 600;
var DEF_COOKIE_EXPIRATION_IN_DAYS = 10;
var DEF_COOKIE_NAME = 'fogalom';
var fogalom_lista = {};

$(document).ready(function () {
 var fogalom_cookie = readCookie(DEF_COOKIE_NAME) || 'be';

	$('div.tartalom span.fogalom')
		.each(function () {
			
			var szoveg = $(this).text();
			var f = $(this).attr('title') ? $(this).attr('title') : szoveg;
			var def = fogalmak[f];
			fogalom_lista[f] = true;
			$(this).data('def', def);
			$(this).removeAttr('title');
			if (fogalom_cookie == 'be') {
				$(this).html(
					'<a  tabindex="0" class="fogalom" title="' +
					def
					+'">'+szoveg+'<img src="css/ures.gif" style="width:0;height:0" alt="Fogalom magyarázata:' +
				     def
					+'"/></a>'
				);
			}
		});

	var fogalom_db = $('div.tartalom span.fogalom').length;
	$('div.tartalom a.fogalom')
		.toggle(show_fogalom,hide_fogalom)	
		.focus(show_fogalom)
		.blur(hide_fogalom);

	if (fogalom_db > 0) {
		
		$('span.ki_fogalomkapcs')
		.replaceWith(
			$('<a accesskey="m" tabindex="25" class="ki_fogalomkapcs" href="#"><img src="navi/ikon_fogalmakbe_dis.gif" alt="Fogalom megjelenítés (nem elérhető funckió)" title="Fogalom megjelenítés (nem elérhető funckió)" width="36" height="36" /></a>')
				);		
		
$('span.ki_fogalomlista"')
		.replaceWith(
			$('<a accesskey="l" tabindex="27" class="ki_fogalomlista" href="#"><img src="navi/ikon_fogalmaklista_dis.gif" alt="Fogalmak listája (nem elérhető funkció)" title="Fogalmak listája (nem elérhető funkció)" width="36" height="36" /></a>')
				);	

		$('.ki_fogalomkapcs')
			.addClass(fogalom_cookie == 'be' ? 'fogalombe' : 'fogalomki')
			.removeClass('ki_fogalomkapcs')
			.html(fogalom_cookie == 'be' ? '<img src="navi/ikon_fogalmakbe.gif" alt="Fogalom megjelenítés bekapcsolva" title="Fogalom megjelenítés bekapcsolva" width="36" height="36" />' : '<img src="navi/ikon_fogalmakki.gif" alt="Fogalom megjelenítés kikapcsolva" title="Fogalom megjelenítés kikapcsolva" width="36" height="36" />')
			
			.click(function () {
				toggle_fogalom_state();
			});
		$('.ki_fogalomlista')
			.addClass('fogalomlista')
			.removeClass('ki_fogalomlista')
			.html('<img src="navi/ikon_fogalmaklista.gif" alt="Fogalomlista megtekintése" title="Fogalomlista megtekintése" width="36" height="36" >')
			
			.click(function (e) {
				var top = (screen.height-(DEF_NEW_WINDOW_HEIGHT + 100))/2;
				var left = (screen.width-DEF_NEW_WINDOW_WIDTH)/2;
				var w = window.open('fogalom_lista.html', '_blank',
					'width='+DEF_NEW_WINDOW_WIDTH+
					',height='+DEF_NEW_WINDOW_HEIGHT+
					',scrollbars=yes'+
					',left='+left+
					',screenX='+left+
					',top='+top+
					',screenY='+top
				);
				e.preventDefault();
			});
	}
	$('a.fogalomlista').focus(function() {  
       $(this).find('span.ikonfelirat_ki').removeClass("ikonfelirat_ki").addClass("ikonfelirat"); 
 });  

 $('a.fogalomlista').blur(function() {  
       $(this).find('span.ikonfelirat').removeClass("ikonfelirat").addClass("ikonfelirat_ki"); 
 });  
 
  $('a.fogalomki').focus(function() {  
       $(this).find('span.ikonfelirat_ki').removeClass("ikonfelirat_ki").addClass("ikonfelirat"); 
 });  

 $('a.fogalomki').blur(function() {  
       $(this).find('span.ikonfelirat').removeClass("ikonfelirat").addClass("ikonfelirat_ki"); 
 });  
 
 $('a.fogalombe').focus(function() {  
       $(this).find('span.ikonfelirat_ki').removeClass("ikonfelirat_ki").addClass("ikonfelirat"); 
 });  

 $('a.fogalombe').blur(function() {  
       $(this).find('span.ikonfelirat').removeClass("ikonfelirat").addClass("ikonfelirat_ki"); 
 });  
 
 
});

function toggle_fogalom_state() {
	if ($('.fogalombe').length > 0) {
		$('.fogalombe')
			.toggleClass('fogalombe')
			.toggleClass('fogalomki')
			.html('<img src="navi/ikon_fogalmakki.gif" alt="Fogalom megjelenítés kikapcsolva" title="Fogalom megjelenítés kikapcsolva" width="36" height="36" />');
		createCookie(DEF_COOKIE_NAME, 'ki', DEF_COOKIE_EXPIRATION_IN_DAYS);

		$('div.tartalom a.fogalom')
			.each(function () {
				$(this).replaceWith($(this).text());
			});
	}
	else {
		$('.fogalomki')
			.toggleClass('fogalombe')
			.toggleClass('fogalomki')
			.html('<img src="navi/ikon_fogalmakbe.gif" alt="Fogalom megjelenítés bekapcsolva" title="Fogalom megjelenítés bekapcsolva" width="36" height="36" />');
		createCookie(DEF_COOKIE_NAME, 'be', DEF_COOKIE_EXPIRATION_IN_DAYS);
		$('div.tartalom span.fogalom')
			.each(function () {
				$(this).html(
					'<a  tabindex="0" class="fogalom" title="' +
					$(this).data('def')
					+'">'+$(this).text()+'<img src="css/ures.gif" style="width:0;height:0" alt="Fogalom magyarázata:'+
					$(this).data('def')
					+'"/></a>'
				);
			});
		$('div.tartalom a.fogalom')
			.toggle(show_fogalom,hide_fogalom)
			.focus(show_fogalom)
			.blur(hide_fogalom);
	}
}

function show_fogalom() {
	if ($('.fogalombe').length > 0) {

		if (!$(this).data('no_need_to_change')) {
			$(this)
				.data('no_need_to_change', true)
				.data('cim', $(this).attr('title'))
				.data('eredeti_tartalom', $(this).html())
				.data('tooltip_tartalom', $(this).data('eredeti_tartalom') +
'<span class="fogalomdoboz"><img src="css/ikon_fogalom.gif" width="30" height="26" style="float:left;margin:0 10px 0 0"><em> Fogalom magyarázata: </em>'+$(this).attr('title')+'</span>');
		}

			if ($(this).css('position') == "relative" && $(this).data('cim')!='') {

			$(this)
				.html($(this).data('tooltip_tartalom'))
				.attr('title', '');
		}
	}
}

function hide_fogalom() {
	if ($('.fogalombe').length > 0) {
		$(this).html($(this).data('eredeti_tartalom'));
		$(this).attr('title', $(this).data('cim'));
	}
}

function get_fogalom_lista() {
	return fogalom_lista;
}



