﻿using System.Text.Json.Serialization;

public class LinkedlnAccessTokenResponseModel
{
    public string access_token { get; set; }
    public long expires_in { get; set; }
    public string refresh_token { get; set; }
    public long refresh_token_expires_in { get; set; }
    public string scope { get; set; }
    public string token_type { get; set; }
    public string id_token { get; set; }

}
