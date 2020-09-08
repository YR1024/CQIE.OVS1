//create by lengyahong (2014-11-09)
//分页控件及排序表格的调用
//参数：paginationID：分页控件ID，sortTableID：排序表格ID，sortField：默认排序字段
function DataPaging(paginationID, sortTableID, sortField) {
    this.SortTableID = sortTableID;
    this.PaginationID = paginationID;
    this.CurTheme = "green"; //当前显示主题
    this.CurSortField = sortField; //排序字段
    this.CurSortType = false; //排序方向,true升序，false降序
 
    //初始化控件，参数:总纪录数|每页显示条数|查询后台数据的异步方法
    this.BindPaging = function (totalCount, pageSize, callBack) {
        //分页事件,考虑了列头的排序字段及方向
        this.InitPagination(totalCount, pageSize, false, null, function (pageIndex) {
            callBack(pageIndex, this.CurSortField, this.CurSortType, false); //默认ID排序，升序排列
        });

        //列头排序，每次排序均从第一页开始显示
        if (this.SortTableID != null && this.SortTableID != "") {
            $("#" + this.SortTableID).tablesorter({ theme: this.CurTheme, sortInitialOrder: "desc" });
            $("#" + this.SortTableID + " thead th").click(function () {
                var sortField = $(this).attr("sortField");
                if (sortField == undefined || sortField == null)
                    return;
                this.CurSortField = sortField;
                this.CurSortType = $(this).hasClass("tablesorter-headerDesc");
                //加载数据，修改排序字段
                if (callBack != null) callBack(0, this.CurSortField, this.CurSortType, true);
            });
        }
    };

    //回调函数,参数:当前页|排序字段|排序方向|是否从新加载分页控件
    this.CallBack = function (curPage, sortField, isDesc, reLoadPaging) {
    //调用页面实现具体操作
    };

    //初始化分页控件，参数：总纪录数|每页显示大小|是否调用回调函数加载数据|初始化指定页（可为空）|页面回调函数
    this.InitPagination = function (totalRecords, pageSize, needLoadData, curentPage, pageCallback) {
        var pagObj = $("#" + this.PaginationID);
        pagObj.pagination(totalRecords, {
            num_edge_entries: 2, //首尾两侧分页显示数
            num_display_entries: 7, //连续分页显示个数
            callback: pageCallback,
            items_per_page: pageSize,
            load_first_page: needLoadData
        });

        //查询第一页内容，页面索引从0开始
        if (totalRecords <= pageSize) {
            pagObj.hide(); //不显示控件
        }
        else {
            pagObj.show(); 
        }

        //加载第curentPage页
        if (curentPage != null && needLoadData == true) {
            pagObj.trigger('setPage', [curentPage]);
        }
    };

    this.SetRowStyle=function()
    {
        $("#" + this.SortTableID + " tr:odd").addClass("lightGray");
        $("#" + this.SortTableID + " tr:even").addClass("deepGray"); 
    }

}   