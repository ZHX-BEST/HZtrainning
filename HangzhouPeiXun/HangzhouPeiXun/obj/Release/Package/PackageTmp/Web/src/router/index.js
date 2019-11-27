import Vue from 'vue'
import Router from 'vue-router'
import TeacherMain from '@/components/page/teacher_main'
import StudentQuestion from '@/components/page/student_question'
import Login from '@/components/page/login'
import Exam from '@/components/page/exam'
import Lssue from '@/components/page/lssue'
import Q_result from '@/components/page/question_result'
import E_result from '@/components/page/exam_result'
import Student from '@/components/page/student_main'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      component: TeacherMain
    },
    {
      path:'/question',
      component:StudentQuestion
    },
    {
      path:'/login',
      component:Login
    },
    {
      path:'/lssue',
      component:Lssue,
    },
    {
      path:'/Qresult',
      component:Q_result,
    },
    {
      path:'/Eresult',
      component:E_result,
    },
    {
      path:'/student',
      component:Student,
    },
    {
      path:'/exam',
      component:Exam,
    }
  ]
})
