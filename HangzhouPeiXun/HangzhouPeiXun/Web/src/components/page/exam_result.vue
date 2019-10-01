<template>
    <div class="Eresult">
        <div class="School_report">
            <div class="report_head">
                <el-select v-model="Semester" placeholder="请选择学期">
                    <el-option v-for="(term,index) in Semesters" :key="index" :label="term.label" :value="term.value">
                    </el-option>
                </el-select>
                <el-select v-model="Class" placeholder="请选择班级">
                    <el-option v-for="(term,index) in Classes" :key="index" :label="term.label" :value="term.value">
                    </el-option>
                </el-select>
            </div>
            <el-table :data="ReportData" :cell-style="{padding:'7px'}">
                <el-table-column :prop="Reports[0].prop" :label="Reports[0].label"></el-table-column>
                <el-table-column :prop="Reports[1].prop" :label="Reports[1].label"></el-table-column>
                <el-table-column :prop="Reports[2].prop" :label="Reports[2].label"></el-table-column>
                <el-table-column :prop="Reports[3].prop" :label="Reports[3].label"></el-table-column>
                <el-table-column :prop="Reports[4].prop" :label="Reports[4].label"></el-table-column>
                <el-table-column :prop="Reports[5].prop" :label="Reports[5].label" sortable></el-table-column>
            </el-table>
            <TBfoot class="TBfoot" :value="TBvalue" ref="tb" :options="TBoptions" @change="change"></TBfoot>
        </div>
        <div class="right">
            <div class="Class_information">
                <h2>{{CalssName}}</h2>
                <div class="classHouse">
                  <i class="iconfont icon-wuzi"></i>
                  <p>教室  {{house}}</p>
                </div>
                <div class="classFuncs">
                  <div v-for="(item,index) in classFuncs" class="classFunc">
                    <p>{{item.text}}</p>
                  </div>
                </div>
            </div>
            <div class="notice">
              <h2>通知</h2>
              <div>{{notice}}</div>
            </div>
        </div>

    </div>
</template>

<script>
import TBfoot from "@/components/common/table_footer";
export default {
    components:{
        TBfoot
    },
  data() {
    return {
      notice:'素材描述：红动网提供UI设计|界面精品原创素材下载，您当前访问作品主题是成绩排名UI界面，编号是9725699,文件格式是PSD，请使用Photoshop CS6及以上版本打开文件，您下载的是一个压缩包文件，请解压后再使用设计软件打开，色彩模式是RGB，，成品尺寸是1920x1080像素，素材大小 是528.01 KB。',
      classFuncs:[
          {
            text:'考勤签到',
          },
          {
            text:'考勤签到',
          },
          {
            text:'考勤签到',
          },
          {
            text:'考勤签到',
          },
      ],
        CalssName:'电气1班',
        house:'102',
        TBvalue:1,
        TBoptions:[1,2],
      Semester: null,
      Semesters: [
        {
          label: "2019年度第一学期",
          value: "20191"
        }
      ],
      Class: null,
      Classes: [
        {
          label: "2019年度第一学期",
          value: "20191"
        }
      ],
      Reports: [
        {
          prop: "num",
          label: "序号"
        },
        {
          prop: "name",
          label: "姓名"
        },
        {
          prop: "class",
          label: "班级"
        },
        {
          prop: "studentid",
          label: "学号"
        },
        {
          prop: "achievement",
          label: "成绩"
        },
        {
          prop: "ranking",
          label: "排名"
        }
      ],
      ReportData: [
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "1"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "2"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "3"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "4"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "5"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "6"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "7"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "8"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "8"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "8"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "7"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "8"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "8"
        },
        {
          num: 1,
          name: "薛翔宇",
          class: "19-1班",
          studentid: "s3457837458",
          achievement: "98",
          ranking: "8"
        },
      ]
    };
  },
  methods:{
      change(){}
  }
};
</script>

<style scoped>
p{
  display: inline;
}
.Eresult{
   /* background-color: rgb(0,104,105); */
   /* height: 92%; */
   /* background-color: #d3d3d3;  */
   /* background-color: rgb(0,11,31);   */
}
.School_report {
  width: 66%;
  float: left;
  padding: 1%;
  padding-left: 3%;
  background-color: #e7eeed; 
  /* background-color: rgb(34,39,59); */
  /* margin-top: 10px; */

}
.right {
  width: 28%;
  margin-left: 1%;
  margin-right: 1%;
  margin-top: 15px;
  float: left;
}
.Class_information{
    /* background-color: #a4b7af; */
     background-color: #e7eeed; 
    overflow: hidden;
}
.classHouse{
  width:30%;
  margin: 0 auto;
  background-color: #f6f6f6;
  padding: 2px;
  color: black;

}
.el-select{
  float: left;
  margin-bottom: 10px;
  margin-right: 10px;
  width: 170px;
  
}
.el-table{
  border: 0;
}
.el-table::before{
  height: 0px;
}
/* .el-select >>> .el-input__inner{
  background-color: rgb(79,82,99) !important;
  color: white;
}
.el-table >>> th{
  background-color: rgb(79,82,99) !important;
  border:0;
}
.el-table >>> tr{
  background-color: rgb(47,43,68);
  color: white;
}
.el-table >>> td{
  border:0;
} */
.TBfoot {
  /* float: right;
  margin-top: 10px;
  margin-right: 40px; */
  margin: 10px auto;
}
.classFuncs{
  width: 70%;
  margin: 30px auto;
  overflow: hidden;
}
.classFunc{
  width:40%;
  height: 20px;
  border-radius: 14px;
  background-color: #d1d7d7;
  padding: 4px;
float: left;
margin-left: 20px;
margin-top: 15px;
}

.notice{
    margin-top: 30px;
    padding-left: 20px;
    padding-right: 20px;
    padding-bottom: 20px;
    /* background-color: #a4b7af; */
     background-color: #e7eeed; 
    overflow: hidden;
}
</style>
