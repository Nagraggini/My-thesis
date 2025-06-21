/*

Based on:
highlight v3
Highlights arbitrary terms.
<http://johannburkard.de/blog/programming/javascript/highlight-javascript-text-higlighting-jquery-plugin.html>
MIT license.
Johann Burkard
<http://johannburkard.de>
<mailto:jb@eaio.com>

Gyozo Horvath

*/

jQuery.fn.highlight = function(pat) {
	function innerHighlight(node, pattern) {
		var pat = pattern.toUpperCase();
		var skip = 0;
		if (node.nodeType == 3) {
			var pos = node.data.toUpperCase().indexOf(pat);
			if (pos >= 0) {
				var spannode;
				if (jQuery(node).parent().is('a.fogalom')) {
					spannode = document.createElement('span');
					spannode.className = 'kereses_kiemeles';
					spannode.title = pattern;
				}
				else {
					spannode = document.createElement('a');
					spannode.className = 'kereses_kiemeles';
					spannode.title = pattern;
					spannode.tabIndex = 0;
					spannode.href = '#';
				}
				var middlebit = node.splitText(pos);
				var endbit = middlebit.splitText(pat.length);
				var middleclone = middlebit.cloneNode(true);
				spannode.appendChild(middleclone);
				middlebit.parentNode.replaceChild(spannode, middlebit);
				skip = 1;
			}
		}
		else if (node.nodeType == 1 && node.childNodes && !/(script|style)/i.test(node.tagName)) {
			for (var i = 0; i < node.childNodes.length; ++i) {
				i += innerHighlight(node.childNodes[i], pattern);
			}
		}
		return skip;
	}
	return this.each(function() {
		innerHighlight(this, pat);
	});
};

jQuery.fn.removeHighlight = function() {
	return this.find(".kereses_kiemeles").each(function() {
		this.parentNode.firstChild.nodeName;
		with (this.parentNode) {
			replaceChild(this.firstChild, this);
			normalize();
		}
	}).end();
};
