$(document).ready(function () {
							
var prot = window.location.protocol;

if (prot == "http:" || prot == "https:" )
{
	$('div.letoltes_doboz')
		.each(function () { 
			var parentTag = $(this).parent();
			var videonev=(parentTag).find('div.felirat').text().replace(/megtekintése,letöltése/,"");
		var azondiv=$(this).children();
		var azon=azondiv.children().attr("id");
		$(this).append(
			$('<div class="videoeszkoztar"><ul><li><a href="#'+azon+'" onclick="'+azon+'.sendEvent(\'PLAY\')">Videó lejátszása/szüneteltetése <span class="jaws">('+videonev+')</span></a></li><li><a href="#'+azon+'" onclick="'+azon+'.sendEvent(\'STOP\')">Videó leállítása <span class="jaws">('+videonev+')</span></a></li><li><a href="#'+azon+'" onclick="'+azon+'.sendEvent(\'VOLUME\', 0);">Hang elnémítása <span class="jaws">('+videonev+')</span></a></li><li><a href="#'+azon+'" onclick="'+azon+'.sendEvent(\'VOLUME\', 50);">Közepes hangerő beállítása <span class="jaws">('+videonev+')</span></a></li><li><a href="#'+azon+'" onclick="'+azon+'.sendEvent(\'VOLUME\', 100);">Teljes hangerő beállítása <span class="jaws">('+videonev+')</span></a></li></ul></div>')
					);
					});
};

if (top.location==self.location) {
	$('.ujablak')
		.removeClass('ujablak')	
 		.addClass('ujablak_ki');
	};

if(window.test){
$('span.elozolap')
		.replaceWith(
			$('<a class="elozolap" href="javascript:history.back();" accesskey="o" tabindex="19"><img src="../../navi/ikon_vissza.gif" alt="Vissza az előzőleg látogatott oldalra" title="Vissza az előzőleg látogatott oldalra"></a>')
				);

$('span.ki_nyomtatas')
		.replaceWith(
			$('<a class="nyomtatas" href="javascript:window.print();" accesskey="y" tabindex="29"><img src="../../navi/ikon_nyomtatas.gif" alt="Oldal nyomtatása"  title="Oldal nyomtatása"/></a>')
				);
}
else{
$('span.elozolap')
		.replaceWith(
			$('<a class="elozolap" href="javascript:history.back();" accesskey="o" tabindex="19"><img src="navi/ikon_vissza.gif" alt="Vissza az előzőleg látogatott oldalra" title="Vissza az előzőleg látogatott oldalra"></a>')
				);

$('span.ki_nyomtatas')
		.replaceWith(
			$('<a class="nyomtatas" href="javascript:window.print();" accesskey="y" tabindex="29"><img src="navi/ikon_nyomtatas.gif" alt="Oldal nyomtatása"  title="Oldal nyomtatása"/></a>')
				);
}



$(function() {
        var xOffset = 0;
        var yOffset = 0;

        $("div.utm_hivatkozasok a").focus(function(e) {
												   
			var $kids = $(this).children().attr("alt");
											   
            this.t = this.title;
            this.title = "";
            $(this).append("<span id='tooltip'>" + $kids + "</span>");
                 });

        $("div.utm_hivatkozasok a").blur(function(e) {
            this.title = this.t;
            $("#tooltip").remove();

            $("#tooltip").css("top", (e.pageY - xOffset) + "px").css("left", (e.pageX + yOffset) + "px");   
        });   
    });




});
	