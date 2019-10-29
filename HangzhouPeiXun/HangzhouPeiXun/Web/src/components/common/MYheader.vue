<template>
    <div>
        <div class="top_nav">
            <img src="@/assets/th.jpg" />
            <h2>防窃电培训平台{{the_title}}</h2>
        </div>
        <div class="button_nav">
            <div class="nav_button" v-for=" (term,index) in nav_buttons" :key="index" @click="Jump(index)">
                <p>{{term}}</p>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name:"MYheader",
    data(){
        return{
            nav_buttons:[
                "典型曲线",
                "课堂练习",
                "在线考试",
                "成绩查询"
            ],
            route:[],
            the_title:"",
            type:null,
        }
    },
    methods:{
        setTitle(value){
            console.log(value)
            this.type=value;
            if(value==1){
                this.the_title="(学生端)";
            }
            else if(value==0){
                this.the_title="(教师端)";
            }
            this.Tojump()
        },
        Tojump(){
            console.log(this.the_title)
        if(this.type==1){
            this.route=[
                '/student',
                '/question',
                '/exam',
                '/Eresult' 
            ];
        }
        else if(this.type==0){
            this.route=[
                '/',
                '/Qresult',
                '/lssue',
                '/Eresult'
            ]
        }
        },
        Jump(index){
            this.$router.push({path:this.route[index]})
        }
    },
    mounted(){
        this.Tojump()
    }
};
</script>

<style>
.top_nav{
 height: 60px;
 text-align: center;
 background-color: #006869;
 line-height: 100%;
 min-width: 800px;
 border-bottom: 1px solid white;
}
.top_nav h2{
    margin:0;
    padding: 0;
    line-height: 80px;
    color:white;
}
.top_nav img{
    height: 100%;
    float: left;
}
.button_nav{
    height: 40px;
    background-color: #025758;
}
.nav_button{ 
    float: left;
    height: 40px;
    line-height: 40px;
    background-color: #034e4e;
    border-right: 1px solid white;
    width: 80px;
    text-align: center;
    color: white;
    cursor: pointer;
}
.nav_button :hover{
    color: #7a87ed;
}
.nav_button p{
    color: white;
    margin: 0;
}
</style>
