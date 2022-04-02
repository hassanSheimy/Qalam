<template>
    <v-container>
        <v-row no-gutters>
           
            <v-col cols="12">
                <custom-card color="primary" :title="$locales.t('timetable')" text="" customClass="white--text px-5 py-3">
                    <calendar-card :events="events" @select="OpenSelectModal" @start="StartLive" />
                </custom-card>
            </v-col>

            <!-- Dialogs -->
            <select-timetable-modal :dialog="selectTimeModal" @done="SelectTimeDone" @close="SelectTimeClose" 
            :start-time="selectedEvent.startTime" :end-time="selectedEvent.endTime" :day="selectedEvent.day"
            :timetable-id="selectedEvent.id" />
        </v-row>
    </v-container>
</template>

<script>
import CustomCard from '@/components/cards/custom-card'
import CalendarCard from '@/components/cards/calendar-card.vue'
import SelectTimetableModal from '@/components/modals/select-timetable-modal.vue'
import moment from 'moment'

export default {
    name: "TeacherTimetable",

    components: {
        CustomCard,
        CalendarCard,
        SelectTimetableModal
    },

    data: () => ({
        selectTimeModal: false,
        events: [],
        selectedEvent: {},
        selectedIndex: -1
    }),

    created() {
        this.GetEvents();
    },

    methods: {
        ApplyFilter(filter){
            this.filter = filter;
            this.GetEvents()
        },
        GetEvents(){
            this.HttpRequest({
                url: 'api/teacher/timetable',
                auth: true
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            response.data.forEach(event => {
                event.startTime = moment.utc(event.startTime, "HH:mm").local().format("HH:mm")
                event.endTime = moment.utc(event.endTime, "HH:mm").local().format("HH:mm")
                event.start = moment.utc(event.start).local().format("YYYY-MM-DD HH:mm")
                event.end = moment.utc(event.end).local().format("YYYY-MM-DD HH:mm")
            })
            this.events = response.data
        },
        GetFailed(error){
            // error happened
        },
        OpenSelectModal(event, index){
            this.selectedEvent = event;
            this.selectedIndex = index;
            this.selectTimeModal = true
        },
        SelectTimeClose(){
            this.selectTimeModal = false
        },
        SelectTimeDone(event){
            Object.assign(this.events[this.selectedIndex], event);
            this.events[this.selectedIndex].isInterval = false;
            this.selectTimeModal = false;
        },
        StartLive(){
            // start live
        }
    }
}
</script>

<style scoped>
</style>