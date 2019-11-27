<template>
  <div class="main">
    <div class="left_nav">
      <div class="left_one" v-for="(term,index) in left_navs" :key="index">
        <p style="font-size:1.2Wem;margin:0; margin-top:10px;">{{term.title}}</p>
        <el-button class="btns" v-for="(btn,bindex) in term.btns" :key="bindex" @click="choose(index,bindex)">{{btn}}</el-button>
      </div>
    </div>
    <div class="right">
      <p class="tle">正常曲线：</p>
      <div id="tu1">
        <!-- <div id="ctable1" style="width: 100%;height: 90%;background-color: #67C23A"></div>
      <div class="mytitle">正常曲线</div> --></div>
      <p class="tle">异常曲线：</p>
      <div id="tu2">
        <!-- <div id="ctable2" style="width: 100%;height: 90%;background-color:#E6A23C"></div>
      <div class="mytitle">异常曲线</div> -->
      </div>
    </div>
    <ErrorC v-if="flag" class="errorC"></ErrorC>
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
      myChart1:null,
      myChart2:null,

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
      buttons: [[], [], []]
    };
  },
  methods: {
    show() {
      this.flag = true;
    },
    showTu1() {
      this.myChart1 = echarts.init(document.getElementById("tu1"));
      var option1 = {
        title: {
          text: "正常曲线",
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
      this.myChart1.setOption(option1);
    },
    showTu2() {
      this.myChart2 = echarts.init(document.getElementById("tu2"));
      var option2 = {
        title: {
          text: "异常曲线",
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
      this.myChart2.setOption(option2);
    },

    closeTu1(){
      this.myChart1.clear()
    },
    closeTu2(){
this.myChart2.clear()
    },
    choose(index, bindex) {
      //用choice记录选择的按钮 如果没有选择就是9
      switch (index) {
        case 0:
          if (this.choice[0] != 9) {
            this.buttons[0]
              .eq(this.choice[0])
              .css("background-color", "#006869");
              this.closeTu1()
              this.closeTu2()
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
                this.closeTu1()
                this.closeTu2()
            }
            if (this.choice[2] != 9) {
              this.buttons[2]
                .eq(this.choice[2])
                .css("background-color", "#006869");
            }
            this.choice[1] = bindex;
            this.buttons[1]
              .eq(this.choice[1])
              .css("background-color", "#075176");
              this.showTu1();
          }
          

          break;
        case 2:
          if (this.choice[0] != 9 && this.choice[1] != 9) {
            if (this.choice[2] != 9) {
              this.buttons[2]
                .eq(this.choice[2])
                .css("background-color", "#006869");
                this.closeTu2()
            }
            this.choice[2] = bindex;
            this.buttons[2]
              .eq(this.choice[2])
              .css("background-color", "#075176");
              
              this.showTu2()
          }
          
          break;
      }
    }
  },
  mounted() {
    $(".left_nav")
      .eq(0)
      .css(
        "height",
        $(".main")
          .eq(0)
          .css("height")
      );
    let left_one = $(".left_one");
    for (let i = 0; i < 3; i++) {
      this.buttons[i] = left_one.eq(i).find(".btns");
    }
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
}
.btn {
  height: 40px;
  line-height: 40px;
  background-color: #034e4e;
  border-right: 1px solid white;
  width: 80px;
  text-align: center;
  color: white;
  float: right;
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
  font-size: 1.3em;
  text-align: left;
  margin-left: 10px;
}
#tu1 {
  min-width: 800px;
  height: 40vh;
}
#tu2 {
  min-width: 800px;
  height: 40vh;
}
.errorC {
  /* display: none; */
}
</style>