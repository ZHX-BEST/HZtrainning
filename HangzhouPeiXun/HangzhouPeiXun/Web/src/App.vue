<template>
  <div id="app">
    <MYheader v-show="!(path==='/login')" ref="myheader"></MYheader>
    <router-view/>
  </div>
</template>

<script>
import MYheader from "@/components/common/MYheader";
export default {
  components: {
    MYheader
  },
  data() {
    return {
      mytitle: "",
      path: ""
    };
  },
  name: "App",
  mounted() {
    this.path = this.$route.path;
    console.log(this.path);
    if (this.path == "/" || this.path == "/Qresult" || this.path == "/lssue") {
      console.log("222");
      let title =0;
      this.$refs.myheader.setTitle(title);
    } else if (
      this.path == "/question" ||
      this.path == "/student" ||
      this.path == "/exam"
    ) {
      let title =1;
      this.$refs.myheader.setTitle(title);
      console.log("11");
    }
  },
  watch: {
    $route(to,form) {
      this.path = to.path;
      console.log(to.path);
      this.path = this.$route.path;
      console.log(this.path);
      if (
        this.path == "/" ||
        this.path == "/Qresult" ||
        this.path == "/lssue"
      ) {
        console.log("00");
        let title = 0;//教师端是0
        this.$refs.myheader.setTitle(title);
      } else if (
        this.path == "/question" ||
        this.path == "/student" ||
        this.path == "/exam"
      ) {
         console.log("11");
        let title = 1;//学生端是1
        this.$refs.myheader.setTitle(title);

      }
    }
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
