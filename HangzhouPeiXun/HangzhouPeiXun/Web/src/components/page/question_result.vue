<template>
    <div>
        <div class="result_head">
            <div>
                <h1>当前互动结果：</h1>
            </div>
            <div>
                <div class="head_container">
                    <i class="iconfont icon-denglu"></i>
                    <p>同学{{student_num}}</p>
                </div>
            </div>
        </div>

        <div v-for="term in comments">
            <div class="result_comment">
                <div class="comment_head">
                    <p class="name">{{term.name}}</p>
                    <p class="time">{{term.time}}</p>
                </div>
                <div class="comment_content">
                    <p>{{term.text}}</p>
                </div>
                <div class="comment_footer"></div>
            </div>
        </div>
        <div class="search" @click="dialogVisible = true">
            <i class="iconfont icon-fangdajing"></i>
        </div>
        <el-dialog title="查询" :visible.sync="dialogVisible" width="30%" :before-close="handleClose">
            <span><el-input v-model="search_name" placeholder="请输入需要查询的学生姓名"></el-input></span>
            <span slot="footer" class="dialog-footer">
                <el-button @click="dialogVisible = false">取 消</el-button>
                <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>
<script>
export default {
  data() {
    return {
      search_name:null,
      dialogVisible: false,
      student_num: 189,
      comments: [
        {
          name: "陈泽旺",
          time: "19/09/19  08:46",
          text:
            "周三上机作业的提交时间延长一天，注意以下问题:图层的设置，线型的分类，图形的对应关系，尺寸的标注。在此作业下提交。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        },
        {
          name: "薛翔宇",
          time: "19/09/19  08:46",
          text: "7月份的时候的波形出现了问题，推测可能是受到了过电压。"
        }
      ]
    };
  },
  methods: {
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    }
  }
};
</script>
<style>
p {
  margin: 0;
  padding: 0;
}
.result_head {
  width: 1200px;
  margin: 10px auto;
  height: 150px;
  background-color: rgb(21, 99, 166);
  border-radius: 10px;
}
.result_head h1 {
  text-align: left;
  padding-left: 30px;
  padding-top: 20px;
  color: white;
}
.head_container {
  padding: 6px;
  color: white;
  float: left;
  margin-left: 20px;

  background-color: rgb(121, 160, 199);
}
.head_container p {
  display: inline;
  margin-left: 4px;
  font-size: 0.8em;
}
.head_container i {
  font-size: 1em;
}
.result_comment {
  width: 1000px;
  margin: 10px auto;
  /* height: 150px; */
  overflow: hidden;
  border: 1px solid rgb(226, 230, 237);
  border-radius: 10px;
  padding: 20px 30px;
}
.comment_head {
  width: 100%;
}
.name {
  float: left;
  font-size: 0.9em;
  background-color: rgb(241, 243, 244);
  padding: 5px;
}
.time {
  float: left;
  padding: 5px;
  margin-left: 20px;
}
.comment_content {
  float: left;
  text-align: left;
  width: 100%;
  font-size: 1.3em;
  margin-top: 20px;
}
.search {
  position: fixed;
  top: 400px;
  right: 40px;
  width: 50px;
  height: 50px;
  line-height: 50px;
  border-radius: 25px;
  background-color: rgb(50, 186, 240);
  cursor: pointer;
}
.search i {
  font-size: 1.4em;
  color: white;
}
</style>
