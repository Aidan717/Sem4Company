using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class IndexStats {

            public _Shards _shards { get; set; }
            public _All _all { get; set; }
            public Indices indices { get; set; }
        

        public class _Shards {
            public int total { get; set; }
            public int successful { get; set; }
            public int failed { get; set; }
        }

        public class _All {
            public Primaries primaries { get; set; }
            public Total total { get; set; }
        }

        public class Primaries {
            public Docs docs { get; set; }
            public Store store { get; set; }
            public Indexing indexing { get; set; }
            public Get get { get; set; }
            public Search search { get; set; }
            public Merges merges { get; set; }
            public Refresh refresh { get; set; }
            public Flush flush { get; set; }
            public Warmer warmer { get; set; }
            public Query_Cache query_cache { get; set; }
            public Fielddata fielddata { get; set; }
            public Completion completion { get; set; }
            public Segments segments { get; set; }
            public Translog translog { get; set; }
            public Request_Cache request_cache { get; set; }
            public Recovery recovery { get; set; }
        }

        public class Docs {
            public int count { get; set; }
            public int deleted { get; set; }
        }

        public class Store {
            public int size_in_bytes { get; set; }
            public int reserved_in_bytes { get; set; }
        }

        public class Indexing {
            public int index_total { get; set; }
            public int index_time_in_millis { get; set; }
            public int index_current { get; set; }
            public int index_failed { get; set; }
            public int delete_total { get; set; }
            public int delete_time_in_millis { get; set; }
            public int delete_current { get; set; }
            public int noop_update_total { get; set; }
            public bool is_throttled { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Get {
            public int total { get; set; }
            public int time_in_millis { get; set; }
            public int exists_total { get; set; }
            public int exists_time_in_millis { get; set; }
            public int missing_total { get; set; }
            public int missing_time_in_millis { get; set; }
            public int current { get; set; }
        }

        public class Search {
            public int open_contexts { get; set; }
            public int query_total { get; set; }
            public int query_time_in_millis { get; set; }
            public int query_current { get; set; }
            public int fetch_total { get; set; }
            public int fetch_time_in_millis { get; set; }
            public int fetch_current { get; set; }
            public int scroll_total { get; set; }
            public int scroll_time_in_millis { get; set; }
            public int scroll_current { get; set; }
            public int suggest_total { get; set; }
            public int suggest_time_in_millis { get; set; }
            public int suggest_current { get; set; }
        }

        public class Merges {
            public int current { get; set; }
            public int current_docs { get; set; }
            public int current_size_in_bytes { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int total_docs { get; set; }
            public int total_size_in_bytes { get; set; }
            public int total_stopped_time_in_millis { get; set; }
            public int total_throttled_time_in_millis { get; set; }
            public int total_auto_throttle_in_bytes { get; set; }
        }

        public class Refresh {
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int external_total { get; set; }
            public int external_total_time_in_millis { get; set; }
            public int listeners { get; set; }
        }

        public class Flush {
            public int total { get; set; }
            public int periodic { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Warmer {
            public int current { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Query_Cache {
            public int memory_size_in_bytes { get; set; }
            public int total_count { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
            public int cache_size { get; set; }
            public int cache_count { get; set; }
            public int evictions { get; set; }
        }

        public class Fielddata {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
        }

        public class Completion {
            public int size_in_bytes { get; set; }
        }

        public class Segments {
            public int count { get; set; }
            public int memory_in_bytes { get; set; }
            public int terms_memory_in_bytes { get; set; }
            public int stored_fields_memory_in_bytes { get; set; }
            public int term_vectors_memory_in_bytes { get; set; }
            public int norms_memory_in_bytes { get; set; }
            public int points_memory_in_bytes { get; set; }
            public int doc_values_memory_in_bytes { get; set; }
            public int index_writer_memory_in_bytes { get; set; }
            public int version_map_memory_in_bytes { get; set; }
            public int fixed_bit_set_memory_in_bytes { get; set; }
            public int max_unsafe_auto_id_timestamp { get; set; }
            public File_Sizes file_sizes { get; set; }
        }

        public class File_Sizes {
        }

        public class Translog {
            public int operations { get; set; }
            public int size_in_bytes { get; set; }
            public int uncommitted_operations { get; set; }
            public int uncommitted_size_in_bytes { get; set; }
            public int earliest_last_modified_age { get; set; }
        }

        public class Request_Cache {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
        }

        public class Recovery {
            public int current_as_source { get; set; }
            public int current_as_target { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Total {
            public Docs1 docs { get; set; }
            public Store1 store { get; set; }
            public Indexing1 indexing { get; set; }
            public Get1 get { get; set; }
            public Search1 search { get; set; }
            public Merges1 merges { get; set; }
            public Refresh1 refresh { get; set; }
            public Flush1 flush { get; set; }
            public Warmer1 warmer { get; set; }
            public Query_Cache1 query_cache { get; set; }
            public Fielddata1 fielddata { get; set; }
            public Completion1 completion { get; set; }
            public Segments1 segments { get; set; }
            public Translog1 translog { get; set; }
            public Request_Cache1 request_cache { get; set; }
            public Recovery1 recovery { get; set; }
        }

        public class Docs1 {
            public int count { get; set; }
            public int deleted { get; set; }
        }

        public class Store1 {
            public int size_in_bytes { get; set; }
            public int reserved_in_bytes { get; set; }
        }

        public class Indexing1 {
            public int index_total { get; set; }
            public int index_time_in_millis { get; set; }
            public int index_current { get; set; }
            public int index_failed { get; set; }
            public int delete_total { get; set; }
            public int delete_time_in_millis { get; set; }
            public int delete_current { get; set; }
            public int noop_update_total { get; set; }
            public bool is_throttled { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Get1 {
            public int total { get; set; }
            public int time_in_millis { get; set; }
            public int exists_total { get; set; }
            public int exists_time_in_millis { get; set; }
            public int missing_total { get; set; }
            public int missing_time_in_millis { get; set; }
            public int current { get; set; }
        }

        public class Search1 {
            public int open_contexts { get; set; }
            public int query_total { get; set; }
            public int query_time_in_millis { get; set; }
            public int query_current { get; set; }
            public int fetch_total { get; set; }
            public int fetch_time_in_millis { get; set; }
            public int fetch_current { get; set; }
            public int scroll_total { get; set; }
            public int scroll_time_in_millis { get; set; }
            public int scroll_current { get; set; }
            public int suggest_total { get; set; }
            public int suggest_time_in_millis { get; set; }
            public int suggest_current { get; set; }
        }

        public class Merges1 {
            public int current { get; set; }
            public int current_docs { get; set; }
            public int current_size_in_bytes { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int total_docs { get; set; }
            public int total_size_in_bytes { get; set; }
            public int total_stopped_time_in_millis { get; set; }
            public int total_throttled_time_in_millis { get; set; }
            public int total_auto_throttle_in_bytes { get; set; }
        }

        public class Refresh1 {
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int external_total { get; set; }
            public int external_total_time_in_millis { get; set; }
            public int listeners { get; set; }
        }

        public class Flush1 {
            public int total { get; set; }
            public int periodic { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Warmer1 {
            public int current { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Query_Cache1 {
            public int memory_size_in_bytes { get; set; }
            public int total_count { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
            public int cache_size { get; set; }
            public int cache_count { get; set; }
            public int evictions { get; set; }
        }

        public class Fielddata1 {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
        }

        public class Completion1 {
            public int size_in_bytes { get; set; }
        }

        public class Segments1 {
            public int count { get; set; }
            public int memory_in_bytes { get; set; }
            public int terms_memory_in_bytes { get; set; }
            public int stored_fields_memory_in_bytes { get; set; }
            public int term_vectors_memory_in_bytes { get; set; }
            public int norms_memory_in_bytes { get; set; }
            public int points_memory_in_bytes { get; set; }
            public int doc_values_memory_in_bytes { get; set; }
            public int index_writer_memory_in_bytes { get; set; }
            public int version_map_memory_in_bytes { get; set; }
            public int fixed_bit_set_memory_in_bytes { get; set; }
            public int max_unsafe_auto_id_timestamp { get; set; }
            public File_Sizes1 file_sizes { get; set; }
        }

        public class File_Sizes1 {
        }

        public class Translog1 {
            public int operations { get; set; }
            public int size_in_bytes { get; set; }
            public int uncommitted_operations { get; set; }
            public int uncommitted_size_in_bytes { get; set; }
            public int earliest_last_modified_age { get; set; }
        }

        public class Request_Cache1 {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
        }

        public class Recovery1 {
            public int current_as_source { get; set; }
            public int current_as_target { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Indices {
            public Schools schools { get; set; }
        }

        public class Schools {
            public string uuid { get; set; }
            public Primaries1 primaries { get; set; }
            public Total1 total { get; set; }
        }

        public class Primaries1 {
            public Docs2 docs { get; set; }
            public Store2 store { get; set; }
            public Indexing2 indexing { get; set; }
            public Get2 get { get; set; }
            public Search2 search { get; set; }
            public Merges2 merges { get; set; }
            public Refresh2 refresh { get; set; }
            public Flush2 flush { get; set; }
            public Warmer2 warmer { get; set; }
            public Query_Cache2 query_cache { get; set; }
            public Fielddata2 fielddata { get; set; }
            public Completion2 completion { get; set; }
            public Segments2 segments { get; set; }
            public Translog2 translog { get; set; }
            public Request_Cache2 request_cache { get; set; }
            public Recovery2 recovery { get; set; }
        }

        public class Docs2 {
            public int count { get; set; }
            public int deleted { get; set; }
        }

        public class Store2 {
            public int size_in_bytes { get; set; }
            public int reserved_in_bytes { get; set; }
        }

        public class Indexing2 {
            public int index_total { get; set; }
            public int index_time_in_millis { get; set; }
            public int index_current { get; set; }
            public int index_failed { get; set; }
            public int delete_total { get; set; }
            public int delete_time_in_millis { get; set; }
            public int delete_current { get; set; }
            public int noop_update_total { get; set; }
            public bool is_throttled { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Get2 {
            public int total { get; set; }
            public int time_in_millis { get; set; }
            public int exists_total { get; set; }
            public int exists_time_in_millis { get; set; }
            public int missing_total { get; set; }
            public int missing_time_in_millis { get; set; }
            public int current { get; set; }
        }

        public class Search2 {
            public int open_contexts { get; set; }
            public int query_total { get; set; }
            public int query_time_in_millis { get; set; }
            public int query_current { get; set; }
            public int fetch_total { get; set; }
            public int fetch_time_in_millis { get; set; }
            public int fetch_current { get; set; }
            public int scroll_total { get; set; }
            public int scroll_time_in_millis { get; set; }
            public int scroll_current { get; set; }
            public int suggest_total { get; set; }
            public int suggest_time_in_millis { get; set; }
            public int suggest_current { get; set; }
        }

        public class Merges2 {
            public int current { get; set; }
            public int current_docs { get; set; }
            public int current_size_in_bytes { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int total_docs { get; set; }
            public int total_size_in_bytes { get; set; }
            public int total_stopped_time_in_millis { get; set; }
            public int total_throttled_time_in_millis { get; set; }
            public int total_auto_throttle_in_bytes { get; set; }
        }

        public class Refresh2 {
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int external_total { get; set; }
            public int external_total_time_in_millis { get; set; }
            public int listeners { get; set; }
        }

        public class Flush2 {
            public int total { get; set; }
            public int periodic { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Warmer2 {
            public int current { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Query_Cache2 {
            public int memory_size_in_bytes { get; set; }
            public int total_count { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
            public int cache_size { get; set; }
            public int cache_count { get; set; }
            public int evictions { get; set; }
        }

        public class Fielddata2 {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
        }

        public class Completion2 {
            public int size_in_bytes { get; set; }
        }

        public class Segments2 {
            public int count { get; set; }
            public int memory_in_bytes { get; set; }
            public int terms_memory_in_bytes { get; set; }
            public int stored_fields_memory_in_bytes { get; set; }
            public int term_vectors_memory_in_bytes { get; set; }
            public int norms_memory_in_bytes { get; set; }
            public int points_memory_in_bytes { get; set; }
            public int doc_values_memory_in_bytes { get; set; }
            public int index_writer_memory_in_bytes { get; set; }
            public int version_map_memory_in_bytes { get; set; }
            public int fixed_bit_set_memory_in_bytes { get; set; }
            public int max_unsafe_auto_id_timestamp { get; set; }
            public File_Sizes2 file_sizes { get; set; }
        }

        public class File_Sizes2 {
        }

        public class Translog2 {
            public int operations { get; set; }
            public int size_in_bytes { get; set; }
            public int uncommitted_operations { get; set; }
            public int uncommitted_size_in_bytes { get; set; }
            public int earliest_last_modified_age { get; set; }
        }

        public class Request_Cache2 {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
        }

        public class Recovery2 {
            public int current_as_source { get; set; }
            public int current_as_target { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Total1 {
            public Docs3 docs { get; set; }
            public Store3 store { get; set; }
            public Indexing3 indexing { get; set; }
            public Get3 get { get; set; }
            public Search3 search { get; set; }
            public Merges3 merges { get; set; }
            public Refresh3 refresh { get; set; }
            public Flush3 flush { get; set; }
            public Warmer3 warmer { get; set; }
            public Query_Cache3 query_cache { get; set; }
            public Fielddata3 fielddata { get; set; }
            public Completion3 completion { get; set; }
            public Segments3 segments { get; set; }
            public Translog3 translog { get; set; }
            public Request_Cache3 request_cache { get; set; }
            public Recovery3 recovery { get; set; }
        }

        public class Docs3 {
            public int count { get; set; }
            public int deleted { get; set; }
        }

        public class Store3 {
            public int size_in_bytes { get; set; }
            public int reserved_in_bytes { get; set; }
        }

        public class Indexing3 {
            public int index_total { get; set; }
            public int index_time_in_millis { get; set; }
            public int index_current { get; set; }
            public int index_failed { get; set; }
            public int delete_total { get; set; }
            public int delete_time_in_millis { get; set; }
            public int delete_current { get; set; }
            public int noop_update_total { get; set; }
            public bool is_throttled { get; set; }
            public int throttle_time_in_millis { get; set; }
        }

        public class Get3 {
            public int total { get; set; }
            public int time_in_millis { get; set; }
            public int exists_total { get; set; }
            public int exists_time_in_millis { get; set; }
            public int missing_total { get; set; }
            public int missing_time_in_millis { get; set; }
            public int current { get; set; }
        }

        public class Search3 {
            public int open_contexts { get; set; }
            public int query_total { get; set; }
            public int query_time_in_millis { get; set; }
            public int query_current { get; set; }
            public int fetch_total { get; set; }
            public int fetch_time_in_millis { get; set; }
            public int fetch_current { get; set; }
            public int scroll_total { get; set; }
            public int scroll_time_in_millis { get; set; }
            public int scroll_current { get; set; }
            public int suggest_total { get; set; }
            public int suggest_time_in_millis { get; set; }
            public int suggest_current { get; set; }
        }

        public class Merges3 {
            public int current { get; set; }
            public int current_docs { get; set; }
            public int current_size_in_bytes { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int total_docs { get; set; }
            public int total_size_in_bytes { get; set; }
            public int total_stopped_time_in_millis { get; set; }
            public int total_throttled_time_in_millis { get; set; }
            public int total_auto_throttle_in_bytes { get; set; }
        }

        public class Refresh3 {
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
            public int external_total { get; set; }
            public int external_total_time_in_millis { get; set; }
            public int listeners { get; set; }
        }

        public class Flush3 {
            public int total { get; set; }
            public int periodic { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Warmer3 {
            public int current { get; set; }
            public int total { get; set; }
            public int total_time_in_millis { get; set; }
        }

        public class Query_Cache3 {
            public int memory_size_in_bytes { get; set; }
            public int total_count { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
            public int cache_size { get; set; }
            public int cache_count { get; set; }
            public int evictions { get; set; }
        }

        public class Fielddata3 {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
        }

        public class Completion3 {
            public int size_in_bytes { get; set; }
        }

        public class Segments3 {
            public int count { get; set; }
            public int memory_in_bytes { get; set; }
            public int terms_memory_in_bytes { get; set; }
            public int stored_fields_memory_in_bytes { get; set; }
            public int term_vectors_memory_in_bytes { get; set; }
            public int norms_memory_in_bytes { get; set; }
            public int points_memory_in_bytes { get; set; }
            public int doc_values_memory_in_bytes { get; set; }
            public int index_writer_memory_in_bytes { get; set; }
            public int version_map_memory_in_bytes { get; set; }
            public int fixed_bit_set_memory_in_bytes { get; set; }
            public int max_unsafe_auto_id_timestamp { get; set; }
            public File_Sizes3 file_sizes { get; set; }
        }

        public class File_Sizes3 {
        }

        public class Translog3 {
            public int operations { get; set; }
            public int size_in_bytes { get; set; }
            public int uncommitted_operations { get; set; }
            public int uncommitted_size_in_bytes { get; set; }
            public int earliest_last_modified_age { get; set; }
        }

        public class Request_Cache3 {
            public int memory_size_in_bytes { get; set; }
            public int evictions { get; set; }
            public int hit_count { get; set; }
            public int miss_count { get; set; }
        }

        public class Recovery3 {
            public int current_as_source { get; set; }
            public int current_as_target { get; set; }
            public int throttle_time_in_millis { get; set; }
        }


    }
}
