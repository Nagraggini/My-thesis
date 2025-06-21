$(document).ready(function () {
    $('div.ki_kereses')
		.addClass('kereses')
		.removeClass('ki_kereses')
		.append(
			$('<form></form>')
				.submit(function (e) {
					e.preventDefault();
					return false;
				})
		)
		.find('form')
			.append(
				$('<input id="bevmezo" accesskey="z" tabindex="9" title="Beviteli mező. A kereséshez legalább 3 karakter hosszú kifejezést kell megadni." type="text" />')
					.data('akt', -1)
					.data('db', 0)
					.keyup(set_disabled)
					.change(set_disabled)
			)
			.append(
				$('<input id="keres" accesskey="k" tabindex="11" type="submit" title="Keresés indítása" />')
					.val('Keres (k)')
					.attr('disabled', 'disabled')
					.click(function () {
						var minta = $(this).prev().val();
						$('body').removeHighlight();
						if (minta.length > 0) {
							$('body').highlight(minta);
						}
 						$('body a.fogalom').data('no_need_to_change', false);

						var talalatdb = $('body a.kereses_kiemeles').length;
						var text = $(this).prev();
						text.data('db', talalatdb);
						if (talalatdb > 0) {
							text.data('akt', 0);
							
				var kiemeles = $('body .kereses_kiemeles:eq(0)');
							if (kiemeles.is('span')) {
								kiemeles.parent().focus();

						}
						else {
							kiemeles.focus();
						}
						}
						else {

							text.data('akt', -1);
						}
						$(this).nextAll(':button').attr(
							'disabled',
							talalatdb > 0 ? '' : 'disabled'
						)
					})
			)
			.append(
				$('<input id="elozo" tabindex="13" accesskey="q" type="button" title="Előző találat"/>')
					.val('Előző (q)')
					.attr('disabled', 'disabled')
					.click(function () {
						kov_talalat(this, -1);
					})
			)
			.append(
				$('<input  id="kovetkezo" tabindex="15" accesskey="w" type="button" title="Következő találat" />')
					.val('Következő (w)')
					.attr('disabled', 'disabled')
					.click(function () {
						kov_talalat(this, 1);
					})
			);
});

function kov_talalat(obj, irany) {
	var text = $(obj).prevAll(':text');
	var db = text.data('db');
	var akt = text.data('akt');
	akt += irany;
	if (akt<0) {
		akt = 0;
	}
	else if (akt >= db) {
		akt = db-1;
	}
	text.data('akt', akt);
		var kiemeles = $('body .kereses_kiemeles:eq('+akt+')');
	if (kiemeles.is('span')) {
		kiemeles.parent().focus();
	}
	else {
		kiemeles.focus();
	}

}

function set_disabled() {
	$(this).next().attr(
		'disabled',
		$(this).val().length > 2 ? '' : 'disabled'
	)
}