<template>
  <div class="main">
    <div class="left_nav">
      <div class="left_one" v-for="(term,index) in left_navs" :key="index">
        <p style="font-size:1.2Wem;margin:0; margin-top:10px;">{{term.title}}</p>
        <el-button class="btns" v-for="(btn,bindex) in term.btns" :key="bindex" @click="choose(index,bindex)">{{btn}}</el-button>
      </div>
    </div>

    <div class="right">
      <div class="btn" @click="show">出题</div>

      <div class="showT">
        <p class="tle" style="color:green">正常曲线：</p>
        <el-tabs v-model="activeName1" @tab-click="handleClick(1)">
          <el-tab-pane label="电压" name="first">
            <div id="normal_voltage" class="chart" style="height:100vw;width:70vw;"></div>
          </el-tab-pane>
          <el-tab-pane label="电流" name="second">
            <div id="normal_current" class="chart" style="height:100vw;width:70vw;"></div>
          </el-tab-pane>
          <el-tab-pane label="功率" name="third">
            <div id="normal_power" class="chart" style="height:100vw;width:70vw;"></div>
          </el-tab-pane>
        </el-tabs>
      </div>

      <div class="showT">
        <p class="tle" style="color:red">异常曲线：</p>
        <el-tabs v-model="activeName2" @tab-click="handleClick(2)">
          <el-tab-pane label="电压" name="first">
            <div id="abnormal_voltage" class="chart"></div>
          </el-tab-pane>
          <el-tab-pane label="电流" name="second">
            <div id="abnormal_current" class="chart"></div>
          </el-tab-pane>
          <el-tab-pane label="功率" name="third">
            <div id="abnormal_power" class="chart"></div>
          </el-tab-pane>
        </el-tabs>
      </div>

      <ErrorC v-if="flag" class="errorC"></ErrorC>
    </div>

  </div>
</template>
<script>
import ErrorC from "@/components/common/error_choose";
export default {
  components: {
    ErrorC
  },
  data() {
    return {
      activeName1: "first",
      activeName2: "first",
      flag: false,
      left_navs: [
        {
          title: "变电类型",
          btns: ["高供高计", "高供低计", "公用配电", "居民用户"]
        },
        {
          title: "用户类别",
          btns: ["餐馆", "医院", "工厂", "学校", "商场"]
        },
        {
          title: "异常类型",
          btns: ["电压断相", "电流失流", "电流分流", "电流不平衡", "电压不平衡"]
        }
      ],
      choice: [9, 9, 9],
      buttons: [[], [], []],
      myChart1_1: null,
      myChart1_2: null,
      myChart1_3: null,
      myChart2_1: null,
      myChart2_2: null,
      myChart2_3: null
    };
  },
  methods: {
    handleClick(index, tab, event) {
      console.log(index);
      switch (index) {
        case 1:
          console.log(index);
          // this.closeTu1();
          this.showTu1();
          break;
        case 2:
          console.log(index);
          this.closeTu2();
          this.showTu2();
          break;
      }
    },
    show() {
      this.flag = true;
    },
    close() {
      this.flag = false;
    },
    initEchart(){
      this.myChart1_1 = echarts.init(document.getElementById("normal_voltage"));
      this.myChart1_2 = echarts.init(document.getElementById("normal_current"));
      this.myChart1_3 = echarts.init(document.getElementById("normal_power"));
    },
    showTu1() {
      // this.myChart1_1 = echarts.init(document.getElementById("normal_voltage"));
      // this.myChart1_2 = echarts.init(document.getElementById("normal_current"));
      // this.myChart1_3 = echarts.init(document.getElementById("normal_power"));
      var option1 = {
        title: {
          // text: "正常曲线",
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
      this.myChart1_1.setOption(option1);
      this.myChart1_2.setOption(option1);
      this.myChart1_3.setOption(option1);
    },
    showTu2() {
      this.myChart2_1 = echarts.init(document.getElementById("abnormal_voltage"));
      this.myChart2_2 = echarts.init(document.getElementById("abnormal_current"));
      this.myChart2_3 = echarts.init(document.getElementById("abnormal_power"));
      var option2 = {
        title: {
          // text: "异常曲线",
          left: "center"
        },
        tooltip: {},
        xAxis: {
          data: ["1月", "2月", "3月", "4月", "5月", "6月", "7月"]
        },
        yAxis: {},
        series: [
          {
            name: "异常",
            type: "line",
            data: [5, 20, 36, 10, 10, 20, 10]
          }
        ]
      };
      this.myChart2_1.setOption(option2);
      this.myChart2_2.setOption(option2);
      this.myChart2_3.setOption(option2);
    },

    closeTu1() {
      this.myChart1_1.dispose();
      this.myChart1_2.dispose();
      this.myChart1_3.dispose();
    },
    closeTu2() {
      this.myChart2_1.dispose();
      this.myChart2_2.dispose();
      this.myChart2_3.dispose();
    },
    choose(index, bindex) {
      //用choice记录选择的按钮 如果没有选择就是9
      switch (index) {
        case 0:
          if (this.choice[0] != 9) {
            this.buttons[0]
              .eq(this.choice[0])
              .css("background-color", "#006869");
            this.closeTu1();
            this.closeTu2();
          }
          if (this.choice[1] != 9) {
            this.buttons[1]
              .eq(this.choice[1])
              .css("background-color", "#006869");
          }
          if (this.choice[2] != 9) {
            this.buttons[2]
              .eq(this.choice[2])
              .css("background-color", "#006869");
          }

          this.choice[0] = bindex;
          this.buttons[0].eq(this.choice[0]).css("background-color", "#075176");
          break;
        case 1:
          if (this.choice[0] != 9) {
            if (this.choice[1] != 9) {
              this.buttons[1]
                .eq(this.choice[1])
                .css("background-color", "#006869");
              this.closeTu1();
              this.closeTu2();
            }
            if (this.choice[2] != 9) {
              this.buttons[2]
                .eq(this.choice[2])
                .css("background-color", "#006869");
            }

            this.showTu1();
            this.choice[1] = bindex;
            this.buttons[1]
              .eq(this.choice[1])
              .css("background-color", "#075176");
          }

          break;
        case 2:
          if (this.choice[0] != 9 && this.choice[1] != 9) {
            if (this.choice[2] != 9) {
              this.buttons[2]
                .eq(this.choice[2])
                .css("background-color", "#006869");
              this.closeTu2();
            }
            this.showTu2();
            this.choice[2] = bindex;
            this.buttons[2]
              .eq(this.choice[2])
              .css("background-color", "#075176");
          }

          break;
      }
    }
  },
  mounted() {
    $(".chart").css(
      "width",
      $(".showT")
        .eq(0)
        .css("width") * 0.8
    );
    // // console.log($(".chart").css("height"));
    // // console.log($(".showT").css("height"));
    $(".chart").css("height", "300px");
    let left_one = $(".left_one");
    for (let i = 0; i < 3; i++) {
      this.buttons[i] = left_one.eq(i).find(".btns");
    }
    this.initEchart()
    console.log(this.buttons);
    // // 生成echart表格;
    // var myChart1 = echarts.init(document.getElementById("tu1"));
    // var myChart2 = echarts.init(document.getElementById("tu2"));
    // var option1 = {
    //   title: {
    //     text: "正常曲线",
    //     left: "center"
    //   },
    //   tooltip: {},
    //   xAxis: {
    //     data: ["1月", "2月", "3月", "4月", "5月", "6月", "7月"]
    //   },
    //   yAxis: {},
    //   series: [
    //     {
    //       name: "正常",
    //       type: "line",
    //       data: [5, 20, 36, 10, 10, 20, 10]
    //     }
    //   ]
    // };
    // myChart1.setOption(option1);
    // var option2 = {
    //   title: {
    //     text: "异常曲线",
    //     left: "center"
    //   },
    //   tooltip: {},
    //   xAxis: {
    //     data: ["1月", "2月", "3月", "4月", "5月", "6月", "7月"]
    //   },
    //   yAxis: {},
    //   series: [
    //     {
    //       name: "异常",
    //       type: "line",
    //       data: [5, 20, 36, 10, 10, 20, 10]
    //     }
    //   ]
    // };
    // myChart2.setOption(option2);
  }
};
</script>
<style scoped>
.main {
  overflow: hidden;
  height: 100%;
}
.left_nav {
  float: left;
  width: 27%;
  height: 100%;
  overflow: hidden;
  background-color: #e4e7ed;
}
.left_one {
  width: 33%;
  float: left;
}
.btns {
  margin-top: 30px;
  width: 80%;
  margin-left: 0;
  background-color: #006869;
  color: white;
  text-align: center;
  overflow: hidden;
}

.right {
  float: left;
  overflow: hidden;
  width: 73%;
  height: 100%;
}
.btn {
  height: 40px;
  line-height: 40px;
  background-color: #034e4e;
  border-right: 1px solid white;
  width: 80px;
  text-align: center;
  color: white;
  position: absolute;
  right: 20px;
  z-index: 10;
  margin: 8px;
  cursor: pointer;
  border-radius: 4px;
  opacity: 0.8;
}
.btn p {
  color: white;
}
.tle {
  font-size: 1.2em;
  text-align: left;
  position: absolute;
  margin-left: 37%;
}
.showT {
  height: 50%;
}
.chart {
  /* width: 100px;
  height: 90px; */
}
.errorC {
  /* display: none; */
}
</style>
