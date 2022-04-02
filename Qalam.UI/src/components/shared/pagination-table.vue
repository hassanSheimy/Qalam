<template>
    <v-card>
        <v-card-title>
            {{title}}
            <v-spacer v-if="title"/>
            <v-text-field
                v-model="search"
                append-icon="fas fa-search"
                :label="$locales.t('search')"
                single-line
                hide-details>
            </v-text-field>
        </v-card-title>
        
        <v-data-table :headers="headers" :items="items" :loading="loading" 
        :loading-text="$locales.t('loading')" 
        :search="search" @pagination="Pagination" :no-data-text="$locales.t('noData')"
        :footer-props="$locales.t('dataIterator')" :server-items-length="totalItems" />
    </v-card>
</template>

<script>
import CustomCard from '@/components/cards/custom-card.vue'
import EmptyState from '@/components/shared/empty-state.vue'

export default {
    name: "PaginationTable",

    components: {
        CustomCard,
        EmptyState
    },

    props: {
        headers: {
            type: Array,
            required: true
        },
        title: {
            type: String,
            required: false
        },
        api: {
            type: Object,
            required: true
        },
        'server-update': {
            type: Boolean,
            default: false
        },
        'map-data': {
            type: Function,
            default: d => d
        }
    },

    data: () => ({
        search: '',
        items: [],
        loading: false,
        pager: {
            page: 1,
            itemsPerPage: 10,
            pageStart: 1,
            pageStop: 10,
            pageCount: 1,
            itemsLength: 8
        },
        totalItems: 0
    }),

    methods: {
        GetData(){
            this.loading = true;
            this.HttpRequest({
                url: this.api.url,
                auth: this.api.auth,
                params: {
                    pageNo: this.pager.page,
                    PageSize: this.pager.itemsPerPage
                },
                loading: false
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },

        GetSuccess(response){
            this.loading = false;
            if(response.data){
                if(this.serverUpdate){

                    this.items = this.mapData(response.data.pageData);
                    this.totalItems = response.data.count;
                }
                else{
                    this.items = this.mapData(response.data);
                    this.totalItems = this.items.length;
                }
            }
        },

        GetFailed(error){
            this.loading = false;
            // error happened
        },

        Pagination(e){
            this.pager = e;
            if(this.serverUpdate){
                this.GetData();
            }
        }
    }
}
</script>