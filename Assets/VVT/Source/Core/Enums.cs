namespace VVT {
    /// <summary> Audio mixers buses </summary>
    public enum Mixer { 
        /// <summary> Every sound final output bus </summary>
        Master, 
        /// <summary> Mixer bus for playing music (e.g. songs, instrumentals, game OST)</summary>
        Music, 
        /// <summary> Mixer bus for playing ambience sounds (e.g. wind) </summary>
        Ambience, 
        /// <summary> Mixer bus for playing sound effects (e.g. jump, shoot, footsteps) </summary>
        SFX 
    }

    /// <summary> Error codes for identifying underlying VVT problems 
    public enum ErrorCode { 
        /// <summary> An unkown generic error happened </summary>
        GenericError = 0,
        /// <summary> An unexpected argument was pass to the function </summary>
        BadArgument = 1,
        /// <summary> Can't get queried resource, the resource is unreachable or null </summary>
        InvalidResource = 2,
    }
}
