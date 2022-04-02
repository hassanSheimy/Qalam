<template>
    <v-container fluid>
        <v-row no-gutters >
            <v-col cols="12">
                <chart-card :data="chartData" :options="chartOptions" color="primary" type="Line" :height="height">
                    <h4 class="title font-weight-light">احصائيات بعدد المشاهدات</h4>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">لأخر 12 شهر</span>
                    </template>
                </chart-card>
            </v-col>
            <v-col md="6" cols="12">
                <rating-component class="ma-2" :data="rateData" title="تقيميات الطلاب للدرس"/>
            </v-col>
            <v-col md="6" cols="12">
                <user-opinion class="ma-2" title="اراء الطلاب في الدرس"/>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import ChartCard from  '@/components/cards/chart-card.vue'
import RatingComponent from '@/components/profile/rating-component.vue'
import UserOpinion from '@/components/profile/user-opinion.vue'

export default {
    name: "TeacherLessonDetails",

    components: {
        ChartCard,
        RatingComponent,
        UserOpinion
    },

    data: () => ({
        height: 400,
        chartData: {
            labels: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001, 2002],
                series: [
                    { data: Array.from({length: 12}, () => Math.floor(Math.random() * 100))}
                ]
        },
        chartOptions: {
            low: 0,
            axisX: {
                showGrid: true,
                showLabel: true,
                offset: 15
            },
            axisY: {
                showGrid: true,
                showLabel: true,
                offset: 0
            },
            height: '400px'
        },
        rateData: [0, 11, 19, 30, 40]
    })
}
</script>

<style scoped>

</style>