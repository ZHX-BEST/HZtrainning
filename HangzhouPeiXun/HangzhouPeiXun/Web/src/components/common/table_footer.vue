<template>
  <div class="TB_footer">
    <el-button @click="left" class="el-buttonTB" type="primary" icon="el-icon-arrow-left">上一页</el-button>
    <el-select class="el-selectTB" v-model="Tvalue" @change="change">
      <el-option v-for="item in Toptions" :value="item" :key='item'>
      </el-option>
    </el-select>
    <el-button @click="right" class="el-buttonTB" type="primary">下一页
      <i class="el-icon-arrow-right el-icon--right"></i>
    </el-button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      Tvalue: this.value,
      Toptions: this.options
    };
  },
  methods: {
    setTvalue(value){
      this.Tvalue=value;
    },
    setToptions(options){
      this.Toptions=options;
    },
    left() {
      if (this.Tvalue > 1) {
        this.Tvalue--;
        this.$emit("change", this.Tvalue);
      }
    },
    right() {
      if (this.Tvalue < this.Toptions.length) {
        this.Tvalue++;
        this.$emit("change", this.Tvalue);
      }
    },
    change() {
      console.log(this.value);
      console.log(this.Tvalue);
      this.$emit("change", this.Tvalue);
    }
  },
  props: ["options", "value"]
};
</script>
<style>
.TB_footer {
  height: 30px;
  width: 200px;
  margin-bottom: 30px;
}
.el-buttonTB {
  float: left;
  height: 30px;
  width: 60px;
  padding: 0;
  font-size: 0.7em;
  background-color: #d1d7d7;
}
.el-selectTB {
  float: left;
  height: 30px;
  width: 65px;
  margin: 0 7px;
}
.el-selectTB > .el-input {
  height: 30px;
}
.el-selectTB > .el-input > input {
  height: 30px;
}
</style>