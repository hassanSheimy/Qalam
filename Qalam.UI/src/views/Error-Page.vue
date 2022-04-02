<template>
    <warning-sign>
        <span class="d-block">
            <template v-for="(text, index) in texts">
                <router-link v-if="index > 0" :key="'router-link-' + index" 
                :to="Link(links[index - 1])" class="remove-underline"
                tag="a" v-text="links[index - 1].text" />
                
                <span :key="'span-' + index" v-html="text" />
            </template>
        </span>
    </warning-sign>
</template>

<script>
import WarningSign from '@/components/shared/warning-sign.vue'

export default {
    name: "ErrorPage",

    components: {
        WarningSign
    },

    props: {
        errorCode: {
            type: Number,
            required: true
        }
    },

    computed: {
        returnUrl() {
            return this.$route.query.returnUrl ? this.$route.query.returnUrl : '/';
        },
        texts(){
            switch(this.errorCode){
                case 401:
                    return this.Edit(this.$locales.t('unauthrizeError.text'))
                    break;
                case 403:
                    return this.Edit(this.$locales.t('forbiddenError.text'))
                    break;
                case 404:
                    return this.Edit(this.$locales.t('notFoundError.text'))
                    break;
                case 500:
                    return this.Edit(this.$locales.t('internalError.text'))
                    break;
                default:
                    return this.Edit(this.$locales.t('notFoundError.text'))
                    break;
            }
        },
        links(){
            switch(this.errorCode){
                case 401:
                    return this.$locales.t('unauthrizeError.links')
                    break;
                case 403:
                    return this.$locales.t('forbiddenError.links')
                    break;
                case 404:
                    return this.$locales.t('notFoundError.links')
                    break;
                case 500:
                    return this.$locales.t('internalError.links')
                    break;
                default:
                    return this.$locales.t('notFoundError.links')
                    break;
            }
        }
    },
    methods: {
        Link(link){
            return `${link.to}${(link.returnUrl ? '?returnUrl=' + this.returnUrl : '')}`;
        },
        Edit(text){
            text = text.replace(/{br}/g, '<br />');
            return text.split("{link}");
        }
    }
}
</script>

<style scoped>
a {
    text-decoration: none;
}
</style>