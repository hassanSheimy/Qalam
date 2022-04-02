<template>
    <v-container fluid>
        <v-row no-gutters >
            <v-col cols="12" class="ma-2">
                <chart-card :data="chartLineData" :options="chartLineOptions" color="primary" type="Line" :height="height">
                    <h4 class="title font-weight-light">احصائيات بمتوسط الدرجات</h4>
                    <template slot="actions">
                        <v-icon class="ma-2" small>fas fa-clock</v-icon>
                        <span class="caption grey--text font-weight-light">كل الوقت</span>
                    </template>
                </chart-card>
            </v-col>

            <v-col cols="12" class="ma-2">
                <pagination-table :headers="headers" title="نتائج الطلاب" text="درجات جميع الطلاب في هذا الإمتحان" :api="api" server-update />
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
import PaginationTable from '@/components/shared/pagination-table.vue'
import RatingComponent from '@/components/profile/rating-component.vue'
import UserOpinion from '@/components/profile/user-opinion.vue'

export default {
    name: "TeacherExamDetails",

    components: {
        ChartCard,
        PaginationTable,
        RatingComponent,
        UserOpinion
    },

    data: () => ({
        height: 400,
        chartLineData: {
            labels: ['0%', '10%', '20%', '30%', '40%', '50%', '60%', '70%', '80%', '90%', '100%'],
            series: [
                { data: Array.from({length: 11}, () => Math.floor(Math.random() * 100)) }
            ]
        },
        chartLineOptions: {
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
        rateData: [0, 11, 19, 30, 40],
        headers: [
            { text: 'التاريخ', value: 'date', sortable: false},
            { text: 'اسم الطالب', value: 'studentName', sortable: false },
            { text: 'الدرجة', value: 'degree', sortable: false }
        ],
        api: {
            url: "api/live/listing",
            auth: true
        }
    })
}
</script>

<style scoped>

</style>