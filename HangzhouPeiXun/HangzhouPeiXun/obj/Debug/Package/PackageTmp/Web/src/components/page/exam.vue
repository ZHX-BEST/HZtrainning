<template>
  <div class="exam">
    <div class="Answer_card">
      <div class="Answer_card_header">
        <div class="title">
          <p>答题卡</p>
        </div>
        <div class="timer">
          <b>{{time}}</b>
        </div>
      </div>
      <div v-for="(term,index) in questionsNum">
        <div class="questionType">
          <span class="qType">
            <b>{{term.label}}</b>
          </span>
          <span class="qnums">
            <p>共{{term.num}}题</p>
          </span>
        </div>
        <div class="questionSerial">
          <div v-for="serial in term.nums" class="serials">
            <p>{{serial}}</p>
          </div>
        </div>
      </div>
    </div>
    <div class="Test_question">
      <div class="Title_number">
        <p>{{NewQuestion.id}}</p>
      </div>
      <div class="Detail_text">
        <p>{{NewQuestion.detail}}</p>
      </div>
      <div id="tu1"></div>
      <div class="footer">
        <div class="timer_footer">
          <b>{{time}}</b>
        </div>
        <div class="submit">
          <p>交卷</p>
        </div>
      </div>
    </div>
    <div class="options">
      <div class="Type">
        <p>{{NewQuestion.type}}</p>
      </div>
      <div class="Title_number">
        <p>{{NewQuestion.id}}</p>
      </div>
      <div class="Title_text">
        <p>{{NewQuestion.text}}</p>
      </div>
      <div class="Single" v-if="single">
        <div class="Single_option" v-for="(term,index) in NewQuestion.options" :key="index">
          <label><input type="radio" name="single" value="index">
            <p>{{term}}</p>
          </label>
        </div>
      </div>
      <div class="Multiple" v-if="multiple">
        <div v-for="(term,index) in NewQuestion.options" class="Multiple_option" :key="index">
          <label><input type="checkbox" name="single" value="index">
            <p>{{term}}</p>
          </label>
        </div>
      </div>
      <div class="Discuss" v-if="discuss">
        <div v-for="(term,index) in NewQuestion.options" class="Discuss_option">
          <table>
            <tr>
              <td>错误类型：</td>
              <td>
                <el-select v-model="errorType" class="errorType">
                  <el-option v-for="(term,index) in errors" :key="index" :value="term" :label="term"></el-option>
                </el-select>
              </td>
            </tr>
            <tr>
              <td>错误时间：</td>
              <td>
                <el-time-select class="errorTime" v-model="timeStart" :picker-options="{start: '00:00',step: '01:00',end: '23:00'}" placeholder="选择时间">
                </el-time-select>
              </td>
            </tr>
            <tr>
              <td>~~~</td>
              <td>
                <el-time-select class="errorTime" v-model="timeEnd" :picker-options="{start: '00:00',step: '01:00',end: '23:00'}" placeholder="选择时间">
                </el-time-select>
              </td>
            </tr>
          </table>
                        <div class="add_option" @click="dialogVisible = true">
                    <p>+</p>
                </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      errorType:null,
      errors:null,
      timeStart:null,
      timeEnd:null,
      single: false,
      multiple: false,
      discuss: true,
      radio: null,
      checkList: null,
      questionsNum: [
        {
          label: "单选题",
          num: 30,
          nums: []
        },
        {
          label: "多选题",
          num: 20,
          nums: []
        },
        {
          label: "论述题",
          num: 10,
          nums: []
        }
      ],
      NewQuestion: {
        type: "单选题",
        mark: "10",
        text: "下列选项中正确的是",
        detail:
          "下面是第三电力公司2019年12月23日的电力分析图，其中可能会有2到4处问题，请你一一找到并完成下面的问题",
        id: "3",
        options: [
          "A.fklsfsdfklasdjfklasdj"
          // "B.flksdjfklajsdoifeasjflk",
          // "C.fkdlsjfioksdjifljsreiltjh",
          // "D.dlfjsklsijpopjp[j"
        ]
      },
      time: "01:13:40"
    };
  },
  mounted() {
    for (let i = 0; i < this.questionsNum.length; i++) {
      for (let j = 1; j < this.questionsNum[i].num; j++) {
        this.questionsNum[i].nums.push(j);
      }
    }
    // 生成echart表格;
    var myChart1 = echarts.init(document.getElementById("tu1"));
    var option1 = {
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
p {
  margin: 0;
  padding: 0;
}
.exam {
  display: flex;
  flex-flow: row nowrap;
  justify-content: space-around;
  align-items: flex-start;
  max-width: 1600px;
  margin: 0 auto;
  position: relative;
  overflow: hidden;
}
.Answer_card {
  width: 290px;
  margin-top: 18px;
  border: 1px solid rgb(228, 228, 228);
  overflow: hidden;
}
.Answer_card_header {
  height: 40px;
}
.title {
  color: white;
  background-color: rgb(56, 159, 195);
  width: 40%;
  height: 100%;
  line-height: 100%;
  float: left;
  font-size: 1.1em;
}
.title p {
  line-height: 40px;
}
.timer {
  color: red;
  background-color: rgb(243, 243, 243);
  width: 60%;
  height: 100%;
  line-height: 100%;
  float: left;
  font-size: 1.2em;
}
.timer b {
  line-height: 40px;
}
.questionType {
  height: 30px;
  width: 90%;
  margin: 0 auto;
  border-bottom: 1px solid rgb(228, 228, 228);
}
.qType {
  float: left;
  width: 50%;
  height: 100%;
}
.qType b {
  line-height: 30px;
}
.qnums {
  float: left;
  width: 50%;
  height: 100%;
  font-size: 0.85em;
}
.qnums p {
  line-height: 30px;
}
.questionSerial {
  width: 100%;
  border-bottom: 1px solid rgb(228, 228, 228);
  overflow: hidden;
  padding: 5px;
}
.serials {
  float: left;
  width: 30px;
  height: 30px;
  border: 1px solid rgb(228, 228, 228);
  margin-left: 10px;
  margin-bottom: 10px;
}
.serials p {
  line-height: 30px;
}

.Test_question {
  width: 800px;
  border: 1px solid rgb(228, 228, 228);
  overflow: hidden;
  position: relative;
}
.Detail_text {
  width: 80%;
  float: left;
  overflow: hidden;
  border-bottom: 1px solid rgb(228, 228, 228);
  margin-top: 10px;
  margin-left: 30px;
  padding: 5px;
  text-align: left;
}
#tu1 {
  min-width: 800px;
  height: 80vh;
}
.footer {
  /* position: absolute; */
  bottom: 0;
  width: 800px;
  height: 40px;
  background-color: rgb(243, 243, 243);
  margin-top: 40px;
}
.timer_footer {
  color: red;
  background-color: rgb(243, 243, 243);
  width: 160px;
  height: 100%;
  line-height: 40px;
  float: left;
  font-size: 1.2em;
}
.submit {
  color: white;
  background-color: rgb(56, 159, 195);
  width: 150px;
  float: right;
  height: 100%;
  line-height: 40px;
  font-size: 1.2em;
  cursor: pointer;
}

.options {
  width: 350px;
  margin-top: 18px;
  border: 1px solid rgb(228, 228, 228);
}
.Type {
  width: 100%;
  min-width: 350px;
  margin-top: 10px;
  height: 40px;
  background-color: rgb(247, 247, 247);
  border-bottom: 3px solid rgb(228, 228, 228);
}
.Type p {
  line-height: 40px;
  margin-right: 200px;
  font-size: 1.3em;
}
.Title_number {
  float: left;
  width: 40px;
  height: 40px;
  border-radius: 20px;
  color: white;
  background-color: rgb(93, 156, 236);
  line-height: 40px;
  font-size: 1.2em;
  margin-top: 10px;
  margin-left: 8px;
}
.Title_text {
  width: 200px;
  float: left;
  overflow: hidden;
  border-bottom: 1px solid rgb(228, 228, 228);
  margin-top: 10px;
  margin-left: 30px;
  padding: 5px;
  text-align: left;
}

.Single {
  width: 90%;
  float: right;
  margin: 0 auto;
}
.Single_option {
  float: left;
  width: 100%;
  height: 50px;
  line-height: 50px;
  /* font-size: 1.4em; */
  text-align: left;
  float: left;
}
.Single_option :hover {
  background-color: rgb(228, 228, 228);
}
.Single_option input {
  margin-top: 15px;
  float: left;
  width: 20px;
  height: 20px;
}

.Multiple {
  width: 90%;
  float: right;
  margin: 0 auto;
}
.Multiple_option {
  float: left;
  width: 100%;
  height: 50px;
  line-height: 50px;
  /* font-size: 1.4em; */
  text-align: left;
  float: left;
}
.Multiple_option :hover {
  background-color: rgb(228, 228, 228);
}
.Multiple_option input {
  margin-top: 15px;
  float: left;
  width: 20px;
  height: 20px;
}

.Discuss {
  width: 90%;
  float: right;
  margin: 0 auto;
}
.Discuss_option{
  width:100%;
  margin-bottom: 18px;
}
.add_option {
  float: left;
  width: 98%;
  height: 40px;
  line-height: 40px;
  font-size: 1.7em;
  opacity: 0.8;
  margin-top: 10px;
  margin-bottom: 10px;
  border: 1px solid rgb(228, 228, 228);
  cursor: pointer;
}
</style>
