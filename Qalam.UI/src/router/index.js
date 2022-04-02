import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'home',
        component: Home
    },
    {
        path: '/live',
        name: 'live',
        component: () => import('../views/Lessons.vue')
    },
    {
        path: '/live/new/:streamKey',
        name: 'new-live',
        component: () => import('../views/Whiteboard.vue')
    },
    {
        path: '/live/watch/:streamKey',
        name: 'live-video',
        component: () => import('../views/Live-Video.vue')
    },
    {
        path: '/lesson/watch/:id',
        name: 'lesson-video',
        component: () => import('../views/Lesson-Video.vue')
    },
    {
        path: '/exam/:id',
        name: 'exam',
        component: () => import('../views/Not-Completed-Page.vue')
    },
    {
        path: '/teachers',
        name: 'teachers',
        component: () => import('../views/Teachers.vue')
    },
    {
        path: '/teacher/:id',
        name: 'teacher-details',
        component: () => import('../views/Not-Completed-Page.vue')
    },
    {
        path: '/contests',
        name: 'contests',
        component: () => import('../views/Not-Completed-Page.vue')
    },
    {
        path: '/packages',
        name: 'packages',
        component: () => import('../views/Not-Completed-Page.vue')
    },
    {
        path: '/signup',
        name: 'signup',
        component: () => import('../views/Signup.vue')
    },
    {
        path: '/signin',
        name: 'signin',
        component: () => import('../views/Signin.vue')
    },
    {
        path: '/about-us',
        name: 'about-us',
        component: () => import('../views/Not-Completed-Page.vue')
    },

    /* Profile Routes */
    {
        path: '/profile/teacher',
        component: () => import('../views/Profile.vue'),
        children: [
            {
                path: '',
                name: 'teacher-timetable',
                component: () => import('../sub-views/Teacher-Timetable.vue')
            },
            {
                path: 'lessons',
                name: 'teacher-lessons',
                component: () => import('../sub-views/Teacher-Lessons.vue')
            },
            {
                path: 'years',
                name: 'teacher-years',
                component: () => import('../sub-views/Teacher-Years.vue')
            },
            {
                path: 'lesson/:id',
                name: 'Teacher-inner-lesson',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'exams',
                name: 'teacher-exams',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'exam/:id',
                name: 'teacher-inner-exam',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'analytics',
                name: 'teacher-analytics',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'settings',
                name: 'teacher-account-settings',
                component: () => import('../sub-views/Profile-Account-Settings.vue')
            }
        ]
    },
    {
        path: '/profile/student',
        component: () => import('../views/Profile.vue'),
        children: [
            {
                path: '',
                name: 'student-lessons',
                component: () => import('../sub-views/Student-Lessons.vue')
            },
            {
                path: 'packages',
                name: 'student-package',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'points',
                name: 'student-points',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'dgrees',
                name: 'student-analytics',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'settings',
                name: 'student-account-settings',
                component: () => import('../sub-views/Profile-Account-Settings.vue')
            }
        ]
    },
    {
        path: '/profile/admin',
        component: () => import('../views/Profile.vue'),
        children: [
            {
                path: '',
                name: 'admin-general-settings',
                component: () => import('../sub-views/Admin-General-Settings.vue')
            },
            {
                path: 'country/:id',
                name: 'admin-country-details',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'timetable',
                name: 'admin-timetable',
                component: () => import('../sub-views/Admin-Timetable.vue')
            },
            {
                path: 'packages',
                name: 'admin-package',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'package/:id',
                name: 'admin-package-details',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'teachers',
                name: 'admin-teachers',
                component: () => import('../sub-views/Admin-Teachers.vue')
            },
            {
                path: 'questions',
                name: 'admin-questions',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'analytics',
                name: 'admin-analytics',
                component: () => import('../views/Not-Completed-Page.vue')
            },
            {
                path: 'settings',
                name: 'admin-account-settings',
                component: () => import('../sub-views/Profile-Account-Settings.vue')
            }
        ]
    },
    /* End Profile Routes */
    {
        path: '/logout',
        name: 'logout',
        component: () => import('../views/Logout.vue')
    },
    /* Error Pages */
    {
        path: '/401',
        name: 'unauthorized',
        component: () => import('../views/Error-Page.vue'),
        props: {
            errorCode: 401
        }

    },
    {
        path: '/403',
        name: 'forbidden',
        component: () => import('../views/Error-Page.vue'),
        props: {
            errorCode: 401
        }
    },
    {
        path: '/404',
        name: 'not-found',
        component: () => import('../views/Error-Page.vue'),
        props: {
            errorCode: 404
        }
    },
    {
        path: '/500',
        name: 'server-error',
        component: () => import('../views/Error-Page.vue'),
        props: {
            errorCode: 500
        }
    },
    {
        path: '*',
        name: 'unknown-path',
        component: () => import('../views/Error-Page.vue'),
        props: {
            errorCode: 404
        }
    },
    /* End Error Pages Routes */
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
})

export default router
