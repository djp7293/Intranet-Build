
$.fn.listenForValueChanging = function (options) {
	var $obj = this;
	valueChangingOptions = { delay: 500 };

	$.extend(valueChangingOptions, options);

		$obj.each(function (i, o) {

			$(o).bind("keyup", function (e) {
				var $target = $(e.target);
				clearTimeout($target.valueChangingTimeout);
				$target.valueChangingTimeout = setTimeout(function () {
					$target.trigger("valueChanging");
				}, valueChangingOptions.delay);
			});
		});
	

		return this;
}