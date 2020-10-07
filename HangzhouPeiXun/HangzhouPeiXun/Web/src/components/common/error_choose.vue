<template>
    <div>
        <div class="Error_main">
          <el-button @click="error_cancel" type="danger" class="error_cancel">取消</el-button>
          <div id="show_tu"></div>
            <div>
                <span>错误类型：</span>
                <el-select v-model="errorType" class="errorType">
                    <el-option v-for="(term,index) in errors" :key="index" :value="term" :label="term"></el-option>
                </el-select>
                <span>错误时间：</span>
                <el-time-select class="errorTime" v-model="timeStart" :picker-options="{start: '00:00',step: '01:00',end: '23:00'}" placeholder="选择时间">
                </el-time-select>
                <el-time-select class="errorTime" v-model="timeEnd" :picker-options="{start: '00:00',step: '01:00',end: '23:00'}" placeholder="选择时间">
                </el-time-select>
                <el-button type="">选择</el-button>
            </div>
            <h3>已添加错误</h3>
            <div style="overflow: hidden;">
                <div v-for="(term,index) in choices" :key="index" class="choices">
                    {{term}}
                    <i class="icon iconfont icon-cuo" @click="close(index)"></i>
                </div>
            </div>

            
        </div>

        <div class="shadow"></div>
    </div>
</template>
<script>
export default {
  data() {
    return {
      errors: ["电压断相", "电流失流", "电流分流", "电流不平衡", "电压不平衡"],
      choices: [
        "电压断相 02:00~08:00",
        "电压断相 02:00~08:00"
        ],
      errorType: null,
      timeStart: null,
      timeEnd: null
    };
  },
  methods: {
    close(index) {},
    error_cancel(){
        this.$parent.close();
    },
  },
  mounted() {
    let top = document.body.clientHeight * 0.08;
    let left = document.body.clientWidth * 0.1;
    $(".Error_main")
      .eq(0)
      .css("top", top + "px");
    $(".Error_main")
      .eq(0)
      .css("left", left + "px");
    $(".shadow")
      .eq(0)
      .css("width", document.body.clientWidth);
    $(".shadow")
      .eq(0)
      .css("height", document.body.clientWidth);
        var myChart1 = echarts.init(document.getElementById("show_tu"));
    var option1 = {
      title: {
        left: "center",
        text:"图像展示"
      },
      tooltip: {},
      xAxis: {
        data: ["1月", "2月", "3月", "4月", "5月", "6月", "7月"]
      },
      yAxis: {},
      series: [
        {
          name: "正常",
          type: "line",
          data: [5, 20, 36, 10, 10, 20, 10]
        }
      ]
    };
    myChart1.setOption(option1);
  }
};
</script>
<style>
.error_cancel{
  position: absolute;
  right: 10px;
  z-index: 100

}
.shadow {
  background-color: #000;
  filter: alpha(opacity=50);
  -moz-opacity: 0.5;
  opacity: 0.5;
  position: fixed;
  left: 0px;
  top: 0px;
  z-index: 1000;
}

.Error_main {
  position: fixed;
  width: 80%;
  margin: 0 auto;
  padding: 10px;
  background-color: white;
  border-radius: 18px;
  box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.2);
  z-index: 1500;
  font-size: 0.9em;
}
.errorType {
  margin-right: 40px;
}
#show_tu {
    width: 100%;
    margin: 0 auto;
    height: 300px;
}
.errorTime {
  width: 100px;
  margin: 0 20px 0 10px;
}
.choices {
  width: 190px;
  height: 20px;
  border: 1px solid #e2e2e2;
  margin: 15px;
  float: left;
  text-align: left;
  line-height: 20px;
  padding: 5px;
  border-radius: 5px;
}
.choices>.icon{
    float: right;
    margin-right: 10px;
    cursor: pointer;
}
</style>
