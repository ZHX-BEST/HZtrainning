登陆
------
url: http://****/login  
get方法  
请求JSON  
{   

    AccountName:"string",  
    PassWord:"string"  
}  
返回JSON  
{  

    Type:'teacher',  
    TeacherId:'t12345',  
    StudentId:"s234235",
}  

*教师端*
===

获取正常数据
--
url:http://****/teacher/getNormalData  
get  
请求JSON  
{  

    TeacherId:'t12345',  
    Substation_type:"",  
    User_type:"",  
}  
返回JSON  
{  

    DATAID：'123456'//正常数据ID  
    Data:[]// 包括下面的数据  
    NormalData:[]//一个数据数组，  
    NormalTime:[]//与数据数组对应的时间数组，要是没有的话就不要了 我写成静态的  
}  

获取异常数据  
--
get  
url:http://****/teacher/getAbnormalData  
  
 请求JSON  
 {  

    TeacherId:'t12345',  
    DATAID：'123456'  
    Abnormal_type:"",  
 }  
 返回JSON  
{  

    AbnormalData:[]//一个数据数组，  
    AbnormalTime:[]//与数据数组对应的时间数组，要是没有的话就不要了 我写成静态的  
}  

出题（课堂练习）  
--
获取正常数据
--
url:http://****/teacher/getNormalData  
get  
请求JSON  
{  

    TeacherId:'t12345',  
    Substation_type:"",  
    User_type:"",  
}  
返回JSON  
{  

    DATAID：'123456'//正常数据ID  
    Data:[]// 包括下面的数据   
    NormalData:[]//一个数据数组，  
    NormalTime:[]//与数据数组对应的时间数组，要是没有的话就不要了 我写成静态的  
}

添加问题（多次叠加）  
--
url:http://****/teacher/setQuestion  
请求JSON  
{  

    TeacherId:'t12345',  
    DATAID：'123456'//正常数据ID  
    Abnormal_type:"",  
    Abnormal_time:"",//异常时间  
}  
返回JSON  
{  

    ABNormalData:[]//一个数据数组，  
    ABNormalTime:[]//与数据数组对应的时间数组  
    Code:"200";
}  

查看做题情况
--
url:http://****/teacher/getQuestionSituation  
请求JSON  
{  

    TeacherId:'t12345',  
}  
返回JSON  
{  

    Data:[
        {
            StudentId:'s43534',
            StudentName:'头头',
            Answer:"string",
        },
        {
            StudentId:'s43534',
            StudentName:'头头',
            Answer:"string",
        },
        {
            StudentId:'s43534',
            StudentName:'头头',
            Answer:"string",
        },
    ]
}

举行考试
--
url:http://****/teacher/setExamination  
post  
请求JSON  
{  

    ExamStartTime:'Date',//格式：yyyy-MM-dd-HH-mm  
    ExamEndTime:'Date',
    TeacherId:'t12345',
    ExaminationName:'电机考试',
    ExaminationPaper:"试卷用什么方式存储不如从长计议",
}
返回JSON  
{  

    Code:'200',
}

学生端
==
学生端与服务器建立websocket，并发送
TeacherId

url:ws://****/student/getMessage  
发送JSON  
{   

    TeacherId:'t12345',
}  

听课(正常数据)
--
返回JSON
{

    Type:"NormalData",
    DATAID:"234234",
    Data:[],//JSON数组 包括数据和时间
}

听课（异常数据）
--
返回JSON
{

    Type:"AbnormalData",
    DATAID:"234234",
    Data:[],//JSON数组 包括数据和时间
}

做题
--
返回JSON
{

    Type:" QuestionData",
    DATAID:"234234",
    Data:[],//JSON数组 包括数据和时间
}

获取考试
--
返回JSON
{

    Type:"Examination",
    ExamStartTime:'Date',//格式：yyyy-MM-dd-HH-mm  
    ExamEndTime:'Date',
    ExaminationName:'电机考试',
}

之后是学生端单独的接口  

提交练习
--
url:http://****/student/submitQuestion
post
请求JSON
{

    StudentId:'s34534',
    text:'这里是学生写的答案，用富文本的形式可以吗？',
}

开始考试
--
url:http://****/student/startExamination
post
请求JSON
{

    StudentId:'s24534',

}
返回JSON
{

    ExaminationPaper:"试卷用什么方式存储不如从长计议",
    RemainderTime:90  //单位 分

}

提交试卷
--
post
请求JSON
{

    Answer:[
        {
            id:1,
            choice:'A',
        },
        {
            id:2,
            choice:'AB',
        },
        ...
    ]
}
返回JSON
{

    Achievement:100,
}