$(document).ready(function () {
    $("#jqg").jqGrid({
        url: '@Url.Action("GetUsers","Menu")',
        datatype: "json",
        colNames: ['User name', 'First name', 'Last name'],
        colModel: [
            { name: 'UserName', index: 'UserName', width: 150, editable: true, edittype: 'text', sortable: true},
            { name: 'firstName', index: 'firstName', width: 150, editable: true, edittype: 'text', sortable: true },
            { name: 'lastName', index: 'lastName', width: 150, editable: true, edittype: 'text', sortable: true },
        ],
        rowNum: 10,
        rowList: [10, 20, 30],
        width: 800,
        pager: '#jpager',
        loadonce: true,
        sortname: 'Id',
        sortorder: "desc",
        caption: "Список пользывателей"
    });
    $("#jqg").jqGrid('navGrid', '#jpager', {

        search: true,
        searchtext: "Поиск",
        refresh: false,
        add: true, // добавление
        del: true, // удаление
        edit: true, // редактирование
        view: true, // просмотр записи
        viewtext: "Смотреть",
        viewtitle: "Выбранная запись",
        addtext: "Добавить",
        edittext: "Изменить",
        deltext: "Удалить"
    },
        update("edit"), // обновление
        update("add"), // добавление
        update("del") // удаление
    );
    function update(act) {
        return {
            closeAfterAdd: true, // закрыть после добавления
            height: 250,
            width: 400,
            closeAfterEdit: true, // закрыть после редактирования
            reloadAfterSubmit: true, // обновление
            drag: true,
            onclickSubmit: function (params) {
                var list = $("#jqg");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                if (act === "add")
                    params.url = '@Url.Action("Create","User")';
                else if (act === "del")
                    params.url = '@Url.Action("Delete","User")';
                else if (act === "edit")
                    params.url = '@Url.Action("Edit","User")';
            },
            afterSubmit: function (response, postdata) {
                // обновление грида
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                return [true, "", 0]
            }
        };
    };
});