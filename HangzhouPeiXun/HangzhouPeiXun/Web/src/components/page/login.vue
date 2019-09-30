<template>
    <div>
        <div class="Login">
            <div class="login_title">
                <h2>浙江防窃电培训平台</h2>
            </div>
            <div class="login_choose">
                <button @click="choice(0)" class="choose">登陆</button>
                <button @click="choice(1)" class="choose">注册</button>
            </div>
            <div class="login_login" v-if="flag">
                <div class="input">
                    <el-input placeholder="请输入账号" v-model="input1">
                        <template slot="prepend">
                            <i class="iconfont icon-denglu"></i>
                        </template>
                    </el-input>
                </div>
                <div class="input">
                    <el-input placeholder="请输入密码" v-model="input2">
                        <template slot="prepend">
                            <i class="iconfont icon-mima"></i>
                        </template>
                    </el-input>
                </div>
                  <el-checkbox class="radio" v-model="radio">记住密码</el-checkbox>
                <span class="span"><a href="#">忘记密码?</a></span>
                <el-button @click="log_in" class="success" type="success">登陆</el-button>
            </div>
            <div class="login_register" v-else>
                <div class="input">
                    <el-input placeholder="请输入邮箱号" v-model="input3">
                        <template slot="prepend">
                            <i class="iconfont icon-denglu"></i>
                        </template>
                    </el-input>
                </div>
                <div class="input">
                    <el-input placeholder="请输入密码" v-model="input4">
                        <template slot="prepend">
                            <i class="iconfont icon-mima"></i>
                        </template>
                    </el-input>
                </div>
                <div class="input">
                    <el-input placeholder="请再次输入密码" v-model="input5">
                        <template slot="prepend">
                            <i class="iconfont icon-mima"></i>
                        </template>
                    </el-input>
                </div>
                <el-button @click="register" type="primary">注册</el-button>

            </div>
        </div>

        <div class="background_image">
            <img src="../../assets/timg.jpg" alt="">
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      flag: true,
      input1: "",
      input2: "",
      input3: "",
      input4: "",
      input5: "",
      radio: true
    };
  },
  methods: {
    choice(index) {
      $(".choose")
        .eq(index)
        .css("background-color", "#17191d");
      $(".choose")
        .eq(!index)
        .css("background-color", "#3c434d");
      this.flag = !index;
    },
    log_in(){
        if(this.input1=="s123456"&&this.input2=="123456"){
          this.$router.push({path:'/student'})
        }
        else if(this.input1=="t123456"&&this.input2=="123456"){
          this.$router.push({path:"/"})
        }
    }
  },
  mounted() {
    $(".choose")
      .eq(0)
      .css("background-color", "#17191d");
  }
};
</script>
<style scoped>
.Login {
  /* margin: 0 auto; */
  left: 40%;
  margin-top: 10%;
  width: 20%;
  min-width: 300px;
  position: absolute;
  z-index: 99;
}
.choose {
  margin: 0;
  width: 50%;
  border: 0;
  padding: 0;
  float: left;
  height: 40px;
  color: white;
  background-color: rgb(60, 67, 77);
  opacity: 0.95;
  border-radius: 1px;
  margin-bottom: 40px;
  outline: none;
  opacity: 0.88;
}
.input {
  margin: 20px auto;
  width: 95%;
  padding: 0;
  overflow: hidden;
  outline: none;
}
.input >>>.el-input-group__prepend{
    border: 0px 
}
.radio {
  margin: 0;
  float: left;
  color: #eceff3;
}
.span {
  float: right;
  
  font-size: 0.em;
}
.span a{
    color: #eceff3;
    text-decoration: none;
}
.el-button {
  float: left;
  width: 100%;
  margin-top: 25px;
  opacity: 0.8;
}
.background_image {
  position: fixed;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  z-index: 1;
}
.background_image img {
  width: 100%;
  height: 100%;
}
</style>
