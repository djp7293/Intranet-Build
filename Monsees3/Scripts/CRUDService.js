function CRUDService(options)
{
    var crudService = this;
    $.extend(this, options);

    crudService.sendAjax=function(ajax) {
        if (!ajax.error) {
            ajax.error = function(e) {
                $.toast("ERROR: " + e.ErrorMessageText);
            }
        }

        ajax.data = $.extend({}, ajax.data);

        //service expecting strings.
        for(var i in ajax.data) {
            ajax.data[i] = JSON.stringify(ajax.data[i]);
        }

        ajax._success = ajax.success;
        ajax.success = function(data) {
            if (data instanceof Object && "ErrorMessageText" in data) { // test for our error message
                ajax.error(data);
            }
            else
            {
                ajax._success(data);
            }
        }

        ajax.url = crudService.baseUrl + "?a=" + ajax.url;
        ajax.type = "POST";
        ajax.dataType = "json";
        $.ajax(ajax);
    }

    

    this.save=function (itemData,options) {
            crudService.sendAjax( $.extend({ url:"Save",data: {item:itemData} }, options) );
        }

    this.delete=function(itemData,options) {
    		crudService.sendAjax($.extend({ url: "Delete", data: { item: itemData } }, options));
        }

   this.getList=function(options) {
   			crudService.sendAjax($.extend({ url: "GetOrderedList" }, options));
        }

}
