var vmPaging = {
    props: ['pageData', 'pageSize'],
    template: "#pagingTpl",
    methods: {
        showPage: function (pageSize) {
            this.$emit('update-pagedata', { pageIndex: 1, pageSize: pageSize });

        }, pageClick: function (tp) {
            if (tp == 'up')
                this.$emit('update-pagedata', { pageIndex: this.cur - 1 });
            else
                this.$emit('update-pagedata', { pageIndex: this.cur + 1 });
        }, btnClick: function (idx) {
            if (idx != this.cur) {
                this.$emit('update-pagedata', { pageIndex: idx });
            }
        }
    }, data: function () {
        return {
            //cur: this.pageData.pageIndex,
            arrPageSize: [10, 20, 30],
            sizeList: this.pageSize,
        }
    }, computed: {
        indexs: function () {
            var left = 1;
            var right = this.all;
            var ar = [];
            if (this.all >= 5) {
                if (this.cur > 3 && this.cur < this.all - 2) {
                    left = this.cur - 2
                    right = this.cur + 2
                } else {
                    if (this.cur <= 3) {
                        left = 1
                        right = 5
                    } else {
                        right = this.all
                        left = this.all - 4
                    }
                }
            }
            while (left <= right) {
                ar.push(left)
                left++
            }
            return ar

        }, all: function () {
            return this.pageData.totalPages
        }, cur: {
            get: function () {
                return this.pageData.pageIndex
            }
        }
    }
}


var vm = new Vue({
    el: "#app",
    data: {
        category: "",
        keywords: "",
        pageData: {},
        categoryData: {},
        pageSize: 10,
        pageIndex: 1,
    }, methods: {
        search: function () {
            this.updatePageData({ keywords: this.keywords, category: this.category, pageIndex: 1 })
        }, edit: function () {


        }, del: function (id) {
            if (confirm("是否确定要删除?")) {
                axios.get('/News/DelNewsAxios', {
                    params: {
                        id: id
                    }
                })
                    .then(function () {
                        vm.updatePageData({});
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                //$.post('/News/DelNewsAxios', { id: id }, function () {
                //    vm.updatePageData({});
                //});
            }
        }, updatePageData: function (payload) {
            if ("pageIndex" in payload)
                this.changeData.pageIndex = payload.pageIndex;
            if ("pageSize" in payload)
                this.changeData.pageSize = payload.pageSize;
            if ("keywords" in payload)
                this.changeData.keywords = payload.keywords;
            if ("category" in payload)
                this.changeData.category = payload.category;
            //$.post("/News/NewsList", this.changeData, function (data) {
            //    vm.pageData = data;
            //});
            axios.post('/News/NewsListAxios', this.changeData)
                .then(function (response) {
                    vm.pageData = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        }

    }, components: {
        'paging-component': vmPaging
    }, mounted: function () {
        axios.post("/News/NewsListAxios", this.changeData)
            .then(function (response) {
                //console.log(response);
                vm.pageData = response.data;
            }).catch(function (error) {
                console.log(error);
            });
        //获取分类目前新闻暂时在分类8，注意修改！！！
        axios.get("/Category/GetCategoryByID", {
            params: {
                categoryid: 8
            }
        })
            .then(function (response) {
                //console.log(response.data);
                vm.categoryData = response.data;
            }).catch(function (error) {
                console.log(error);
            });
    }, computed: {
        changeData: function () {
            return {
                keywords: this.keywords,
                category: this.category,
                pageSize: this.pageSize,
                pageIndex: this.pageIndex,
            }

        }
    }, filters: {
        category: function (value) {
            let result = '';
            //console.log(vm.categoryData);
            for (let i in vm.categoryData) {
                if (value == vm.categoryData[i].categoryID) {
                    result = vm.categoryData[i].categoryName;
                    break;
                }
            }
            return result;
        }, language: function (value) {
            if (value == 0)
                return "中文";
            else
                return "English";

        }, formatDate(time) {
            var date = new Date(time);
            return formatDate(date, "yyyy-MM-dd hh:mm");
        }
    }
});

