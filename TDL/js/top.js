function setExclusiveRadioButton(name, current) {
	regex = new RegExp(name);
	for (i = 0; i < document.forms[0].elements.length; i++) {
		var elem = document.forms[0].elements[i];

		if (elem.type == 'radio' && elem.id.match(regex)) {
			elem.checked = false;
		}
	}
	current.checked = true;
}
	