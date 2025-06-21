$(document).ready(function () {


$('div.ki_stilus')
			.removeClass('ki_stilus')	
			.addClass('stilus');
$('div.stilus')
		.append(
			$('<span class="felirat">Stílus:</span><a accesskey="a" rel="alap" class="styleswitch" href="#" title="Alapértelmezett stílus"> <span class="gyorsb">A</span>lap</a><a accesskey="n" rel="nagy" class="styleswitch" href="#" title="Nagybetűs stílus"> <span class="gyorsb">N</span>agy</a> <a accesskey="i" rel="inverz" class="styleswitch" href="#" title="Inverz stílus nagy betűkkel"><span class="gyorsb">I</span>nverz</a>')
				);							
	$.stylesheetInit();
	$('.styleswitch').click(function(e) {
		$.stylesheetSwitch(this.getAttribute('rel'));
		e.preventDefault();
	});
})

