<template>
    <v-container>
        <v-row no-gutters>
            <v-col cols="12">
                <v-btn color="primary" @click="SaveTimetable(null, -1)">{{$locales.t('addTimetableBtn')}}</v-btn>
            </v-col>

            <v-col cols="12">
                <custom-card color="primary" :title="$locales.t('timetable')" text="" customClass="white--text px-5 py-3">
                    <filter-component @filter="ApplyFilter" />

                    <calendar-card with-crud @delete="DeleteTimetable" @edit="SaveTimetable" :events="events" />
                </custom-card>
            </v-col>

            <!-- Dialogs -->
            <save-timetable-modal :dialog="saveTimetableDialog" @done="ModalDone" @close="ModalClosed" />
            <delete-timetable-modal :dialog="deleteDialog" @done="DeleteDone" @close="DeleteClose" :id="selectedTimetableId" />
        </v-row>
    </v-container>
</template>

<script>
import CustomCard from '@/components/cards/custom-card'
import FilterComponent from '@/components/shared/filter-component.vue'
import CalendarCard from '@/components/cards/calendar-card.vue'
import SaveTimetableModal from '@/components/modals/save-timetable-modal.vue'
import DeleteTimetableModal from '@/components/modals/delete-timetable-modal.vue'
import moment from 'moment'

export default {
    name: "AdminTimetable",

    components: {
        CustomCard,
        FilterComponent,
        CalendarCard,
        SaveTimetableModal,
        DeleteTimetableModal
    },

    data: () => ({
        saveTimetableDialog: false,
        deleteDialog: false,
        events: [],
        filter: {},
        isEdit: false,
        selectedIndex: -1,
        selectedTimetableId: -1
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
                url: 'api/timetable',
                params: {
                    countryId: this.filter.country,
                    educationTypeId: this.filter.educationType,
                    educationYearId: this.filter.educationYear,
                    courseId: this.filter.subject
                },
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
        SaveTimetable(event = null, index = -1){
            this.isEdit = (event != null);
            this.selectedIndex = index;
            this.saveTimetableDialog = true;
            this.$eventBus.$emit("modal-open", event);
        },
        DeleteTimetable(eventId = -1, index = -1){
            this.selectedIndex = index;
            this.deleteDialog = true;
            this.selectedTimetableId = eventId;
        },
        ModalDone(event){
            if(this.isEdit)
                Object.assign(this.events[this.selectedIndex], event);
            else
                this.events.push(event);

            this.saveTimetableDialog = false;
        },
        ModalClosed(){
            this.saveTimetableDialog = false;
        },
        DeleteDone() {
            this.events.splice(this.selectedIndex, 1);
            this.deleteDialog = false;
        },
        DeleteClose() {
            this.deleteDialog = false;
        },
    }
}
</script>

<style scoped>
</style>