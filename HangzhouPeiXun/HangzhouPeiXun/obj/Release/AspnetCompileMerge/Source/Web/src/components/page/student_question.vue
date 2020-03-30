<template>
    <div>
        <div id="errorTu"></div>
        <p class="error_point">请指出图中的错误：</p>
        <div id="WangEditor"></div>
        <div style="width:70%;margin:0 auto">
            <el-button type="success" style="margin-top:10px;float:left">提交</el-button>
            <el-button type="" style="margin-top:10px;float:left">重置</el-button>
        </div>
    </div>
</template>
<script>
import WangEditor from "wangeditor";
export default {
  data() {
    return {
      editor: null
    };
  },

  mounted() {
    this.editor = new WangEditor("#WangEditor"); //这个地方传入div元素的id 需要加#号
    // 配置 onchange 事件
    this.editor.change = function() {
      console.log(this.txt.html());
    };
    this.editor.create(); // 生成编辑器
    this.editor.txt.html("<p>输入错误类型...</p>"); //注意：这个地方是txt  不是官方文档中的$txt

    var myChart1 = echarts.init(document.getElementById("errorTu"));
    var option1 = {
      title: {
        left: "center"
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

<style scoped>
#errorTu {
  width: 95%;
  margin: 0 auto;
  height: 400px;
}
#WangEditor {
  width: 70%;
  text-align: left;
  margin: 0 auto;
}
.w-e-text-container {
  height: 150px !important;
}
.error_point{
  text-align: left;
  width:80%;
  margin: 0 auto;
  font-size: 1.4em;
  margin-bottom: 20px;
  color: red;
  

}
</style>
