CREATE TABLE public.team_member
(
	team_member_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	team_id uuid NOT NULL,
	user_id uuid NOT NULL,
	join_time timestamp with time zone,

	CONSTRAINT team_member_pkey PRIMARY KEY (team_member_id),
	CONSTRAINT team_member_team_id_fkey FOREIGN KEY (team_id) REFERENCES public.team (team_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT team_member_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.user (user_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH ( OIDS = FALSE ) TABLESPACE pg_default;

ALTER TABLE public.team_member OWNER to onlinerecleague_dbuser;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.team_member TO onlinerecleague_dbuser;

CREATE INDEX team_member_team_id_idx ON public.team_member USING btree (team_id) TABLESPACE pg_default;
CREATE INDEX team_member_user_id_idx ON public.team_member USING btree (user_id) TABLESPACE pg_default;
